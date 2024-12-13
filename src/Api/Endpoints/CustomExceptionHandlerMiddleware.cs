using Api.Extensions;
using ValidationException = Xerris.DotNet.Core.Validations.ValidationException;
namespace Api.Endpoints;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> logger;

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
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return context.Response.WriteAsJsonAsync(new
        {
            context.Response.StatusCode,
            Message = "An error occurred while processing your request.",
            DetailedMessage = exception.Message
        });
    }
}
