using Database.Migrations.Constants;
using FluentMigrator.Runner.VersionTableInfo;

namespace Database.Migrations;

[VersionTableMetaData]
public class VersionTableMetaData : IVersionTableMetaData
{
    public bool OwnsSchema => true;
    public string SchemaName => Schemas.Public;
    public string TableName => "_migration_version_info";
    public string ColumnName => "version";
    public string DescriptionColumnName => "description";
    public string UniqueIndexName => "_migration_version_idx";
    public string AppliedOnColumnName => "applied_on";
    public bool CreateWithPrimaryKey => true;
}