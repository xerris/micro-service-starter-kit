using Services.Constants;
using Services.Context;
using Services.Context.Queries;
using Services.Services.Domain;
using Xerris.DotNet.Core.Validations;

namespace Services.Services;

public interface ICustomerService
{
    Task<Customer> Create(Customer toCreate);
}

public class CustomerService : ICustomerService
{
    private readonly IDbContextFactory dbContextFactory;

    public CustomerService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Customer> Create(Customer toCreate)
    {
        var dbContext = dbContextFactory.Create();

        var exists = await dbContext.Customers.FindByName(toCreate.Name);
        Validate.Begin().IsNull(exists, $"Customer: {toCreate.Name} already exists").Check();

        var created = await dbContext.Customers.AddAsync(toCreate);
        await dbContext.SaveChangesAsync();
        return created.Entity;
    }
}