using Xerris.DotNet.Core;

namespace Services;

public interface IApplicationConfig
{
    public LocalDbConfig LocalDbConfig { get; set; }
}

public class ApplicationConfig : IApplicationConfig, IApplicationConfigBase
{
    public LocalDbConfig LocalDbConfig { get; set; } = null!;
}

public class LocalDbConfig
{
    public string Server { get; set; } = "localhost";
    public string DbName { get; set; } = string.Empty;
    public string DbPort { get; set; } = string.Empty;
    public string DbUser { get; set; } = string.Empty;
    public string DbPassword { get; set; } = string.Empty;
    
    public string ConnectionString =>
        $"Server={Server};Port={DbPort};Database={DbName};User Id={DbUser};Password={DbPassword};";
}