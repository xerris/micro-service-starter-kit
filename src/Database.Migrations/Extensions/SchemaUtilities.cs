using System.Text;
using Database.Migrations.Constants;

namespace Database.Migrations.Extensions;

public static class SchemaUtilities
{
    public static string GenerateDropSchemaStatement(this string schemaName) 
        => $"DROP SCHEMA IF EXISTS {schemaName} CASCADE;";

    public static string GenerateCreateSchemaStatement(this string schemaName) 
        => $"CREATE SCHEMA IF NOT EXISTS {schemaName};";

    public static string GeneratePermissionsStatement(this string schemaName)
    {
        var sb = new StringBuilder();
        sb.Append($"GRANT USAGE ON SCHEMA {schemaName} TO {Roles.ReadOnly};");
        sb.Append($"GRANT SELECT ON ALL TABLES IN SCHEMA {schemaName} TO {Roles.ReadOnly};");
        sb.Append($"ALTER DEFAULT PRIVILEGES IN SCHEMA {schemaName} GRANT SELECT ON TABLES TO {Roles.ReadOnly};");

        sb.Append($"GRANT USAGE, CREATE ON SCHEMA {schemaName} TO {Roles.ReadWrite};");
        sb.Append($"GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA {schemaName} TO {Roles.ReadWrite};");
        sb.Append($"GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA {schemaName} TO {Roles.ReadWrite};");
        sb.Append($"ALTER DEFAULT PRIVILEGES IN SCHEMA {schemaName} GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO {Roles.ReadWrite};");
        sb.Append($"ALTER DEFAULT PRIVILEGES IN SCHEMA {schemaName} GRANT USAGE ON SEQUENCES TO {Roles.ReadWrite};");

        return sb.ToString();
    }
}