using Database.Migrations.Constants;
using Database.Migrations.Extensions;
using FluentMigrator;
using Services.Constants;

namespace Skillz.Database.Migrations.Migrations;

[Migration(202412080933)]
public class _202412080933_CreateCustomer : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Customer)
            .InSchema(Schemas.StarterKit)
            .WithId()
            .WithColumn("name").AsString(50).NotNullable()
            .WithIsDeleted()
            .WithVersion()
            .WithAuditColumns();
        
        Create.Index("IX_Customer_Name_IsDeleted")
            .OnTable(Tables.Customer)
            .InSchema(Schemas.StarterKit)
            .OnColumn("name").Ascending()
            .OnColumn("is_deleted").Ascending()
            .WithOptions().Unique();

        Create.Table(Tables.CustomerContact)
            .InSchema(Schemas.StarterKit)
            .WithId()
            .AddIdColumnWithForeignKey(Tables.CustomerContact, "customer_id", Schemas.StarterKit, Tables.Customer)
            .WithColumn("first_name").AsString(50).NotNullable()
            .WithColumn("last_name").AsString(50).NotNullable()
            .WithColumn("email").AsString(50).NotNullable()
            .WithIsDeleted()
            .WithVersion()
            .WithAuditColumns();
    }

    public override void Down()
    {
        Delete.Table(Tables.CustomerContact).InSchema(Schemas.StarterKit);
        Delete.Table(Tables.Customer).InSchema(Schemas.StarterKit);
    }
}