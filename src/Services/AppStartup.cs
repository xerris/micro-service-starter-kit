using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Services.Context;
using Xerris.DotNet.Core;
using Xerris.DotNet.Data;

namespace Services;

public class AppStart : IAppStartup
{
    public IConfiguration StartUp(IServiceCollection collection)
    {
        var builder = new ApplicationConfigurationBuilder<ApplicationConfig>();
        var appConfig = builder.Build();

        collection.AddSingleton<IApplicationConfig>(appConfig);
        collection.AddSingleton<IConnectionStringProvider, LocalConnectionStringProvider>();
        collection.AddSingleton<IConnectionBuilder, ConnectionBuilder>();
        collection.AddSingleton<IDbContextFactory, DbContextFactory>();
        collection.AutoRegister(GetType().Assembly);
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return builder.Configuration;
    }

    public void InitializeLogging(IConfiguration configuration, Action<IConfiguration> defaultConfig)
        => Log.Logger = new LoggerConfiguration()
            .WriteTo
            .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
}
