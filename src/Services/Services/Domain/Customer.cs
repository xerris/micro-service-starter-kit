using Xerris.DotNet.Data.Domain;

namespace Services.Services.Domain;

public class Customer : AuditImmutableBase
{
    public string Name { get; set; } = string.Empty;
    public ICollection<CustomerContact> Contacts { get; set; } = [];
}