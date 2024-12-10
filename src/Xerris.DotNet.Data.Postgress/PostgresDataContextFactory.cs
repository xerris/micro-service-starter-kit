using Microsoft.EntityFrameworkCore;

namespace Xerris.DotNet.Data.Postgres;

public class PostgresDataContextFactory<T> : DataContextFactory<T> where T : DbContext
{
    public PostgresDataContextFactory(IConnectionBuilder connectionBuilder,
        Func<DbContextOptions<T>, T> contextBuilder) : base(connectionBuilder, contextBuilder)
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