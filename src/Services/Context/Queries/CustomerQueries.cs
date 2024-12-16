using Microsoft.EntityFrameworkCore;
using Services.Services.Domain;
using Xerris.DotNet.Data.Extensions;

namespace Services.Context.Queries;

public static class CustomerQueries
{
    public static async Task<Customer?> FindByName(this DbSet<Customer> customers, string name)
        => await customers.FirstOrDefaultAsync(x => x.Name == name && x.IsDeleted == false);

    public static async Task<List<Customer>> GetCustomersAsync(this DbSet<Customer> customers, string nameFilter,
        int page = 1, int pageSize = 25, bool? includeContacts = false)
    {
        return await customers
            .WhereIf(!string.IsNullOrEmpty(nameFilter), c => c.Name.Contains(nameFilter))
            .IncludeIf(includeContacts ?? false, c => c.Contacts)
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Name)
            .ToPaginatedListAsync(page, pageSize);
    }
}