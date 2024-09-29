using Microsoft.EntityFrameworkCore;

namespace Skillz.Services.Context;

public interface IDataContextFactory
{
    DataContext CreateDbContext();
}


public class DataContextFactory : IDataContextFactory
{
    private readonly IConnectionBuilder connectionBuilder;

    // ReSharper disable once ConvertToPrimaryConstructor
    public DataContextFactory(IConnectionBuilder connectionBuilder)
    {
        this.connectionBuilder = connectionBuilder;
    }

    public DataContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        optionsBuilder.UseNpgsql(connectionBuilder.AdminConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            })
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: false)
            .EnableDetailedErrors();

        return new DataContext(optionsBuilder.Options);
    }
}