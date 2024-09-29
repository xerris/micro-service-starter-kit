using FluentMigrator;

namespace Skillz.Database.Migrations.Migrations;

[Migration(202409230731)]
public class _202409230731_AddUuidExtension : Migration
{
    public override void Up() =>
        Execute.Sql(@"CREATE EXTENSION IF NOT EXISTS ""uuid-ossp"";");

    public override void Down() =>
        Execute.Sql(@"DROP EXTENSION IF EXISTS ""uuid-ossp"";");
}