using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Services;
using Services.Context;
using Xerris.DotNet.Core;
using Xerris.DotNet.Data;
using IConnectionBuilder = Xerris.DotNet.Data.IConnectionBuilder;
using IConnectionStringProvider = Xerris.DotNet.Data.IConnectionStringProvider;

namespace Database.Migrations;

public class AppStart : IAppStartup
{
    public IConfiguration StartUp(IServiceCollection collection)
    {
        var builder = new ApplicationConfigurationBuilder<ApplicationConfig>();
        var appConfig = builder.Build();

        collection.AddSingleton<IApplicationConfig>(appConfig);
        collection.AutoRegister(GetType().Assembly);

        collection.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(CreateConnectionString)
                .ScanIn(GetType().Assembly).For.Migrations())
            .AddLogging(lb => lb.AddSerilog(dispose: true))
            .Configure<RunnerOptions>(opt => opt.Tags = [builder.Configuration["stageName"]])
            .AddTransient<IVersionTableMetaData, CustomMetadataTable>();

        collection.AddSingleton<IConnectionStringProvider, LocalConnectionStringProvider>();
        collection.AddSingleton<IConnectionBuilder, ConnectionBuilder>();

        return builder.Configuration;
    }

    public void InitializeLogging(IConfiguration configuration, Action<IConfiguration> defaultConfig)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}");

        Log.Logger = loggerConfiguration.CreateLogger();
    }

    private static string CreateConnectionString(IServiceProvider sp) =>
        sp.GetRequiredService<IConnectionBuilder>().AdminConnectionString;
}