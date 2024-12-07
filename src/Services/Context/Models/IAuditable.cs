namespace Services.Context.Models;

public interface IDeleteable
{
    public bool IsDeleted { get; set; }
}

public interface IAuditable
{
    public Guid Id { get; set; }
    DateTime CreatedOn { get; set; }
    int? CreatedBy { get; set; }
    DateTime? ModifiedOn { get; set; }
    int? ModifiedBy { get; set; }
    public int Version { get; set; }
    public DateTime SynchronizedOn { get; set; }
}

public abstract class AuditImmutableBase : IAuditable, IDeleteable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? ModifiedBy { get; set; }
    public int Version { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime SynchronizedOn { get; set; }
}