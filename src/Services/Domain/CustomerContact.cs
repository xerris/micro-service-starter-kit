using Services.Context.Models;

namespace Services.Domain;

public class CustomerContact : AuditImmutableBase
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}