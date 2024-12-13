using Api.Extensions;
using ValidationException = Xerris.DotNet.Core.Validations.ValidationException;

namespace Api.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> logger;
    private readonly IDictionary<int, string> statusCodeMessages = new Dictionary<int, string>
    {
        { StatusCodes.Status400BadRequest, "A validation error occurred while processing your request." },
        { StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request." }
        // Add more status codes and messages as needed
    };

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            logger.LogError(ex, "Validation error: {@Errors}", ex.FriendlyPrint());
            await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var message = statusCodeMessages.TryGetValue(statusCode, out var customMessage)
            ? customMessage
            : "An error occurred while processing your request.";

        return context.Response.WriteAsJsonAsync(new
        {
            context.Response.StatusCode,
            Message = message,
            DetailedMessage = exception.Message
        });
    }
}