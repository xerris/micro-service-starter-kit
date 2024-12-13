using Microsoft.EntityFrameworkCore;
using Services.Services.Domain;

namespace Services.Context.Queries;

public static class CustomerQueries
{
    public static async Task<Customer?> FindByName(this DbSet<Customer> customers, string name)
        => await customers.FirstOrDefaultAsync(x => x.Name == name);

}