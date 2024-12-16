namespace Api.Models.Responses.Customer;

public class AddCustomerResponse : AuditableDto
{
    public string Name { get; set; } = string.Empty;
}