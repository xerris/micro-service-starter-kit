namespace Api.Models.Responses;

public class AddCustomerResponse : AuditableDto
{
    public string Name { get; set; } = string.Empty;
}