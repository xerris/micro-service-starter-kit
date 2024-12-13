namespace Api.Models;

public class AuditableDto
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public int Version { get; set; } = 1;
}