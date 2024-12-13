using FluentMigrator.Runner.VersionTableInfo;
using Services.Constants;

namespace Database.Migrations;

[VersionTableMetaData]
public class VersionTableMetaData : IVersionTableMetaData
{
    public bool OwnsSchema => true;
    public string SchemaName => Schemas.Public;
    public string TableName => "_migration_version_info";
    public string ColumnName => "version";
    public string DescriptionColumnName => "description";
    public string UniqueIndexName => "_idx_migration_version";
    public string AppliedOnColumnName => "applied_on";
    public bool CreateWithPrimaryKey => true;
}