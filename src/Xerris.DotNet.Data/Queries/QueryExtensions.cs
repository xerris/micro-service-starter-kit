using Microsoft.EntityFrameworkCore;
using Xerris.DotNet.Data.Domain;

namespace Xerris.DotNet.Data.Queries;

public static class QueryExtensions
{
    public static async Task<T?> FindById<T>(this DbSet<T> dbSet, Guid id) 
        where T : class, IAuditable
        => await dbSet.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
}