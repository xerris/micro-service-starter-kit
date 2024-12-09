using Services.Context.Models;

namespace Services.Domain;

public class Customer : AuditImmutableBase
{
    public string Name { get; set; }
    public ICollection<CustomerContact> Contacts { get; set; } = [];
}