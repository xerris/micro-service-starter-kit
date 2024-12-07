using FluentMigrator.Runner.VersionTableInfo;

namespace Database.Migrations;

[VersionTableMetaData]
public class CustomMetadataTable : IVersionTableMetaData
{
    public object ApplicationContext { get; set; } = null!;
    public bool OwnsSchema => true;
    public string SchemaName => string.Empty;
    public string TableName => "_migration_version_info";
    public string ColumnName => "version";
    public string DescriptionColumnName => "description";
    public string AppliedOnColumnName => "applied_on";
    public bool CreateWithPrimaryKey => true;
    public string UniqueIndexName => "uc_version";
}