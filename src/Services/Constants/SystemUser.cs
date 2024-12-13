using Xerris.DotNet.Data.Domain;

namespace Services.Constants;

public class SystemUser : User
{
    public SystemUser()
    {
        Id = Guid.Parse("c9fbd8a1-0043-4427-a4cb-bbf296467562");
        FirstName = "SYSTEM";
        IsServiceAccount = true;
    }
    
    public static User User => new SystemUser();
}

public class User : AuditImmutableBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = null!;
    public bool IsServiceAccount { get; set; }
}