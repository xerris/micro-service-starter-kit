using Database.Migrations.Constants;
using Database.Migrations.Extensions;
using FluentMigrator;
using Services.Constants;

namespace Skillz.Database.Migrations.Migrations;

[Migration(202409230733)]
public class _202409230733_CreateStartKitSchema : Migration
{
    public override void Up() => this.AddSchemaWithPermissions(Schemas.StarterKit);
    public override void Down() => this.DropSchema(Schemas.StarterKit);
}