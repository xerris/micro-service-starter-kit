using Microsoft.EntityFrameworkCore;

namespace Xerris.DotNet.Data.Postgres;

public abstract class PostgresDbContextFactory<T> : DbContextFactory<T> where T : DbContext
{
    public PostgresDbContextFactory(IConnectionBuilder connectionBuilder) : base(connectionBuilder)
    {
    }

    protected override DbContextOptions<T> ApplyOptions(bool sensitiveDataLoggingEnabled = false)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new DbContextOptionsBuilder<T>().UseNpgsql(ConnectionBuilder.AdminConnectionString,
                                                          sqlOptions => { sqlOptions.EnableRetryOnFailure(); })
            .UseSnakeCaseNamingConvention()
            .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: sensitiveDataLoggingEnabled)
            .EnableDetailedErrors()
            .Options;
    }
}