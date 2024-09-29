using FluentMigrator;
using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create.Table;
using Skillz.Database.Migrations.Utilities;

namespace Skillz.Database.Migrations.Extensions;

public static class MigrationExtensions
{
    public static void AddSchemaWithPermissions(this Migration migration, string schema)
    {
        if (migration.Schema.Schema(schema).Exists()) return;

        migration.Execute.Sql(schema.GenerateCreateSchemaStatement());
        migration.Execute.Sql(schema.GeneratePermissionsStatement());
    }

    public static void DropSchema(this Migration migration, string schema)
    {
        migration.Execute.Sql(schema.GenerateDropSchemaStatement());
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithId(this ICreateTableWithColumnSyntax syntax, string columnName = "id")
        => syntax.WithColumn(columnName).AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid);

    public static ICreateTableColumnOptionOrWithColumnSyntax WithUniqueName(this ICreateTableWithColumnSyntax syntax, int size = 256)
        => syntax.WithColumn("name").AsString(size).NotNullable().Unique();

    public static ICreateTableColumnOptionOrWithColumnSyntax WithDescription(this ICreateTableWithColumnSyntax syntax)
        => syntax.WithColumn("description").AsString().NotNullable();

    public static ICreateTableColumnOptionOrWithColumnSyntax WithVersion(this ICreateTableWithColumnSyntax syntax)
        => syntax.WithColumn("version").AsInt32().NotNullable().WithDefaultValue(1);

    public static ICreateTableColumnOptionOrWithColumnSyntax WithIsDeleted(this ICreateTableWithColumnSyntax syntax)
        => syntax.WithColumn("is_deleted").AsBoolean().NotNullable().WithDefaultValue(false);

    public static ICreateTableColumnOptionOrWithColumnSyntax WithIsActive(this ICreateTableWithColumnSyntax syntax)
        => syntax.WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(false);

    public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AddIsActive(this IAlterTableAddColumnOrAlterColumnSyntax syntax)
        => syntax.AddColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(false);

    public static ICreateTableColumnOptionOrForeignKeyCascadeOrWithColumnSyntax AddMandatoryIdColumnWithForeignKey(
        this ICreateTableWithColumnSyntax syntax,
        string fromTableName,
        string fromColumnName,
        string schema,
        string toTableName,
        string keyNameSuffix = "")
        => syntax.WithColumn(fromColumnName).AsGuid().NotNullable().ForeignKey($"fk_{fromTableName}_{toTableName}{keyNameSuffix}", schema, toTableName, "id");

    public static IAlterTableColumnOptionOrAddColumnOrAlterColumnOrForeignKeyCascadeSyntax AddIdColumnWithForeignKey(
        this IAlterTableAddColumnOrAlterColumnSyntax syntax,
        string fromTableName,
        string fromColumnName,
        string toSchema,
        string toTableName,
        string keyNameSuffix = "")
        => syntax.AddColumn(fromColumnName).AsGuid().ForeignKey($"fk_{fromTableName}_{toTableName}{keyNameSuffix}", toSchema, toTableName, "id");

    public static IAlterTableColumnOptionOrAddColumnOrAlterColumnOrForeignKeyCascadeSyntax AddMandatoryIdColumnWithForeignKey(
        this IAlterTableAddColumnOrAlterColumnSyntax syntax,
        string fromTableName,
        string fromColumnName,
        string schema,
        string toTableName,
        string keyNameSuffix = "")
        => syntax.AddColumn(fromColumnName).AsGuid().NotNullable().ForeignKey($"fk_{fromTableName}_{toTableName}{keyNameSuffix}", schema, toTableName, "id");
}