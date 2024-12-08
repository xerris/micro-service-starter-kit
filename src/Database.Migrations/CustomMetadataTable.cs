using Database.Migrations.Constants;
using FluentMigrator.Runner.VersionTableInfo;

namespace Database.Migrations;

[VersionTableMetaData]
public class CustomMetadataTable : DefaultVersionTableMetaData
{
    public override bool OwnsSchema => true;
    public override string SchemaName => Schemas.Public;
    public override string TableName => "_migration_version_info";
    public override string ColumnName => "version";
    public override string DescriptionColumnName => "description";
    public override string AppliedOnColumnName => "applied_on";
}