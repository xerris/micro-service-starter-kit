using Services.Context;
using Services.Services.Domain;

namespace Services.Services;

public interface ICustomerService
{
    Task<Customer> CreateCustomer(Customer toCreate);
}

public class CustomerService : ICustomerService
{
    private readonly IDbContextFactory dbContextFactory;

    public CustomerService(IDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public Task<Customer> CreateCustomer(Customer toCreate)
    {
        var dbContext = dbContextFactory.Create();

        throw new NotImplementedException();
    }
}