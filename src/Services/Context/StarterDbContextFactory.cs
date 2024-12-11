using Microsoft.EntityFrameworkCore;
using Xerris.DotNet.Data;
using Xerris.DotNet.Data.Postgres;

namespace Services.Context;

public interface IDbContextFactory
{
    StarterDbContext Create();
}

public class DbContextFactory : PostgresDbContextFactory<StarterDbContext>, IDbContextFactory
{
    public DbContextFactory(IConnectionBuilder connectionBuilder) : base(connectionBuilder)
    {
    }

    protected override StarterDbContext Create(DbContextOptions<StarterDbContext> options)
    {
        return new StarterDbContext(options);
    }
}