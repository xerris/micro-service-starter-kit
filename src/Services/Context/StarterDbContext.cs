using Microsoft.EntityFrameworkCore;
using Services.Constants;
using Services.Context.Config;
using Services.Services.Domain;
using Xerris.DotNet.Data;

namespace Services.Context;

public class StarterDbContext(DbContextOptions<StarterDbContext> options) : DbContext<StarterDbContext>(options)
{
    public DbSet<Customer> Customers { get; private set; }
    
    protected override void RegisterModels(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.StarterKit);
        modelBuilder.ApplyConfiguration(new CustomerConfig());
    }
}