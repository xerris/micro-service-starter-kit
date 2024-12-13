using Microsoft.EntityFrameworkCore;
using Services.Constants;
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
        var context = new StarterDbContext(options);
        context.WithUserId(SystemUser.User.Id); //the default audit user
        return context;
    }
}