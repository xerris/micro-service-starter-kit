using FluentMigrator.Runner.VersionTableInfo;

namespace Skillz.Database.Migrations;

[VersionTableMetaData]
public class CustomMetadataTable : IVersionTableMetaData
{
    public object ApplicationContext { get; set; }
    public bool OwnsSchema => true;
    public string SchemaName => string.Empty;
    public string TableName => "_migration_version_info";
    public string ColumnName => "version";
    public string DescriptionColumnName => "description";
    public string AppliedOnColumnName => "applied_on";
    public string UniqueIndexName => "uc_version";
}