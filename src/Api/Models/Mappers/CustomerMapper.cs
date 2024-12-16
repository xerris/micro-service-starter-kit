using Api.Models.Requests.Customer;
using Api.Models.Responses.Customer;
using Services.Services.Domain;
using Xerris.DotNet.Data.Domain;

namespace Api.Models.Mappers;

public static class CustomerMapper
{
    public static Customer ToCustomer(this AddCustomerRequest request) => new() { Name = request.Name };

    public static AddCustomerResponse Created(this Customer customer)
        => new AddCustomerResponse { Name = customer.Name, Version = 1 }.ApplyAuditFields(customer);

    public static IEnumerable<CustomerModel> ToModels(this List<Customer> customers)
        => customers.Select(ToModel);
    
    public static CustomerModel ToModel(this Customer customer)
        => new CustomerModel
        {
            Name = customer.Name
        }.ApplyAuditFields(customer);
    
    private static T ApplyAuditFields<T, TA>(this T dto, TA auditable) where T : AuditableDto
        where TA : IAuditable
    {
        dto.Id = auditable.Id;
        dto.CreatedBy = auditable.CreatedBy ;
        dto.CreatedOn = auditable.CreatedOn;
        dto.ModifiedBy = auditable.ModifiedBy;
        dto.ModifiedOn = auditable.ModifiedOn;
        dto.Version = auditable.Version;
        return dto;
    }
}
