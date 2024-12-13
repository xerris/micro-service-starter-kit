using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xerris.DotNet.Data.Domain;

namespace Services.Context.Config;

public static class EFConfigExtensions
{
    public static void ConfigureImmutableBase<T>(this EntityTypeBuilder<T> builder, string tableName) where T: AuditImmutableBase
    {
        builder.ToTable(tableName);
        builder.HasKey(x => x.Id);
        builder.Property(p => p.CreatedOn).HasColumnName("created_on").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(p => p.ModifiedOn).HasColumnName("modified_on");
        builder.Property(p => p.ModifiedBy).HasColumnName("modified_by");
        builder.Property(p => p.Version).HasColumnName("version").IsRequired();
        builder.Property(p => p.IsDeleted).HasColumnName("is_deleted").IsRequired();
    }
}