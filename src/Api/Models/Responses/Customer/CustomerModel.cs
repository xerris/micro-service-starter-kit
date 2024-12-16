namespace Api.Models.Responses.Customer;

public class CustomerModel : AuditableDto
{
    public string Name { get; set; } = string.Empty;
}