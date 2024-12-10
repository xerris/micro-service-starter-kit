using Microsoft.EntityFrameworkCore;

namespace Xerris.DotNet.Data;

public interface IDataContextFactory<T> where T : Microsoft.EntityFrameworkCore.DbContext
{
    T CreateDbContext();
}

public abstract class DataContextFactory<T> : IDataContextFactory<T> where T : Microsoft.EntityFrameworkCore.DbContext
{
    protected readonly IConnectionBuilder ConnectionBuilder;
    private readonly Func<DbContextOptions<T>, T> contextBuilder;

    // ReSharper disable once ConvertToPrimaryConstructor
    public DataContextFactory(IConnectionBuilder connectionBuilder, Func<DbContextOptions<T>, T> contextBuilder)
    {
        ConnectionBuilder = connectionBuilder;
        this.contextBuilder = contextBuilder;
    }

    public T CreateDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var options = ApplyOptions();
        return contextBuilder(options);
    }

    protected abstract DbContextOptions<T> ApplyOptions(bool sensitiveDataLoggingEnabled = false);
}