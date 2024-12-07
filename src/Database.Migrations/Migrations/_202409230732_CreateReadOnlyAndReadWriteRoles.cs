using System.Text;
using Database.Migrations.Constants;
using FluentMigrator;

namespace Skillz.Database.Migrations.Migrations;

[Migration(202409230732)]
public class _202409230732_CreateReadOnlyAndReadWriteRoles : Migration
{
    public override void Up()
    {
        var sb = new StringBuilder();
        sb.Append($"create role {Roles.ReadOnly};");
        sb.Append($"grant connect on database {Databases.StarterKit} to {Roles.ReadOnly};");
        sb.Append($"grant usage on schema {Schemas.Public} to {Roles.ReadOnly};");
        sb.Append($"grant select on all tables in schema {Schemas.Public} to {Roles.ReadOnly};");
        sb.Append($"alter default privileges in schema {Schemas.Public} grant select on tables to {Roles.ReadOnly};");

        sb.Append($"create role {Roles.ReadWrite};");
        sb.Append($"grant connect on database {Databases.StarterKit} to {Roles.ReadWrite};");
        sb.Append($"grant usage, create on schema {Schemas.Public} to {Roles.ReadWrite};");
        sb.Append($"grant all privileges on all sequences in schema {Schemas.Public} to {Roles.ReadWrite};");
        sb.Append($"grant select, insert, update, delete on all tables in schema {Schemas.Public} to {Roles.ReadWrite};");
        sb.Append($"alter default privileges in schema {Schemas.Public} grant select, insert, update, delete on tables to {Roles.ReadWrite};");
        sb.Append($"alter default privileges in schema {Schemas.Public} grant usage on sequences to {Roles.ReadWrite};");

        Execute.Sql(sb.ToString());
    }

    public override void Down()
    {
        DropPrivilegesFor(Roles.ReadOnly);
        DropPrivilegesFor(Roles.ReadWrite);
    }

    private void DropPrivilegesFor(string roleName)
    {
        var sb = new StringBuilder();
        sb.Append($"alter default privileges in schema {Schemas.Public} revoke select, insert, update, delete on tables from {roleName};");
        sb.Append($"alter default privileges in schema {Schemas.Public} revoke all on sequences from {roleName};");
        sb.Append($"alter default privileges in schema {Schemas.Public} revoke all on functions from {roleName};");
        sb.Append($"revoke all privileges on all sequences in schema {Schemas.Public} from {roleName};");
        sb.Append($"revoke all privileges on all tables in schema {Schemas.Public} from {roleName};");
        sb.Append($"revoke all privileges on all functions in schema {Schemas.Public} from {roleName};");
        sb.Append($"revoke all privileges on all tables in schema {Schemas.Public} from {roleName};");
        sb.Append($"revoke usage, create on schema {Schemas.Public} from {roleName};");
        sb.Append($"revoke connect on database {Databases.StarterKit} from {roleName};");
        sb.Append($"drop role {roleName};");

        Execute.Sql(sb.ToString());
    }
}