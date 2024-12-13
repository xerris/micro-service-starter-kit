using System.Text.Json;
using Api.Endpoints;
using Api.Extensions;
using Api.Middleware;
using Serilog;
using Services;
using Xerris.DotNet.Core;

var builder = WebApplication.CreateBuilder(args);

IoC.ConfigureServiceCollection(builder.Services);
var appConfig = IoC.Resolve<IApplicationConfig>();

try
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.AddServerHeader = false;
        options.AllowSynchronousIO = false;
    });

    if (builder.Environment.IsProduction())
    {
        builder.Logging.ClearProviders();
    }

    builder.Host.UseConsoleLifetime(options => options.SuppressStatusMessages = true);

    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();
    builder.Services.AddFastEndpoints();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDefaultExceptionHandler();
    }

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<CustomExceptionHandlerMiddleware>();

    app.UseFastEndpoints(options =>
    {
        options.Errors.ResponseBuilder = (errors, _, _) => errors.ToResponse();
        options.Errors.StatusCode = StatusCodes.Status422UnprocessableEntity;
        options.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

    if (app.Environment.IsDevelopment())
    {
     //   app.UseOpenApi();
     //   app.UseSwaggerUi(x => x.ConfigureDefaults());
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Error("Shut down complete");
    Log.CloseAndFlush();
}