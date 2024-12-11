using Microsoft.EntityFrameworkCore;
using Xerris.DotNet.Data;

namespace Services.Context;

public class StarterDbContext(DbContextOptions<StarterDbContext> options) : DbContext<StarterDbContext>(options)
{
    protected override void RegisterModels(ModelBuilder modelBuilder)
    {
        
    }
}