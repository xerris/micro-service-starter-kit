using Services.Context;
using Services.Context.Queries;
using Services.Services.Domain;
using Xerris.DotNet.Core.Validations;
using Xerris.DotNet.Data.Queries;

namespace Services.Services;

public interface ICustomerService
{
    Task<Customer> CreateAsync(Customer toCreate);
    Task DeleteAsync(Guid id);

    Task<List<Customer>> GetCustomersAsync(string nameFilter, int page = 1,
        int pageSize = 25, bool? includeContacts = false);
} 

public class CustomerService : ICustomerService
{
    private readonly IDbContextFactory dbContextFactory;

    public CustomerService(IDbContextFactory dbContextFactory)
        => this.dbContextFactory = dbContextFactory;

    public async Task<Customer> CreateAsync(Customer toCreate)
    {
        return await DoAsync<Customer>(async dbContext =>
        {
            var exists = await dbContext.Customers.FindByName(toCreate.Name);
            Validate.Begin().IsNull(exists, $"Customer: '{toCreate.Name}' already exists").Check();

            var created = await dbContext.Customers.AddAsync(toCreate);
            return created.Entity;
        });
    }

    public async Task DeleteAsync(Guid id)
    {
        await DoAsync(async dbContext =>
        {
            var toDelete = await dbContext.Customers.FindById(id);
            Validate.Begin().IsNotNull(toDelete, $"Customer with id: {id}' not found").Check();
            toDelete!.IsDeleted = true;
            return id;
        });
    }

    public async Task<List<Customer>> GetCustomersAsync(string nameFilter, int page = 1,
        int pageSize = 25, bool? includeContacts = false)
        => await GetAll(dbContext =>
            dbContext.Customers.GetCustomersAsync(nameFilter, page, pageSize, includeContacts));

    private async Task<T> DoAsync<T>(Func<StarterDbContext, Task<T>> action)
    {
        var dbContext = dbContextFactory.Create();
        var result = await action(dbContext);
        await dbContext.SaveChangesAsync();
        return result;
    }

    private async Task<List<T>> GetAll<T>(Func<StarterDbContext, Task<List<T>>> action)
    {
        var dbContext = dbContextFactory.Create();
        return await action(dbContext);
    }
}