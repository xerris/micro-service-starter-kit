using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Constants;
using Services.Services.Domain;

namespace Services.Context.Config;

public class CustomerContactConfig : IEntityTypeConfiguration<CustomerContact>
{
    public void Configure(EntityTypeBuilder<CustomerContact> builder)
    {
        builder.ConfigureImmutableBase(Tables.CustomerContact);
        builder.Property(p => p.FirstName).HasColumnName("first_name");
        builder.Property(p => p.LastName).HasColumnName("last_name");
        builder.Property(p => p.Email).HasColumnName("email");
        builder.Property(p => p.PhoneNumber).HasColumnName("phone_number");
        builder.Property(p => p.CustomerId).HasColumnName("customer_id");
        
        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId);
    }
}