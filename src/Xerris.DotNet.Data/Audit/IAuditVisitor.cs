using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xerris.DotNet.Core.Time;
using Xerris.DotNet.Data.Domain;

namespace Xerris.DotNet.Data.Audit;

public interface IAuditVisitor
{
    void AcceptNew(EntityEntry entry);
    void AcceptModified(EntityEntry entry);
    void AcceptDeleted(EntityEntry entry);
    Guid? UserId { get; set; }
}

public class AuditVisitor : IAuditVisitor
{
    public void AcceptNew(EntityEntry entry)
    {
        if (entry.Entity is not IAuditable entityEntry) return;

        entityEntry.CreatedOn = Clock.Utc.Now;
        entityEntry.ModifiedOn = Clock.Utc.Now;

        if (!UserId.HasValue) return;
        
        entityEntry.ModifiedBy = UserId;
        entityEntry.CreatedBy ??= UserId.Value;
    }

    public void AcceptModified(EntityEntry entry)
    {
        if (entry.Entity is not IAuditable entityEntry) return;
        entityEntry.ModifiedOn = Clock.Utc.Now;

        if (!UserId.HasValue) return;
        entityEntry.ModifiedBy = UserId;
    }

    public void AcceptDeleted(EntityEntry entry)
    {
        if(entry.Entity is not IAuditable auditableEntry) return;
        
        ((IDeleteable) entry.Entity).IsDeleted = true;
        entry.State = EntityState.Modified;
        auditableEntry.ModifiedOn = Clock.Utc.Now;

        if (!UserId.HasValue) return;
        auditableEntry.ModifiedBy = UserId.Value;
    }

    public Guid? UserId { get; set; }
}