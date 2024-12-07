using Xerris.DotNet.Core;

namespace Services;

public interface IApplicationConfig
{
    public string DbName { get; set; }
    public string DbPort { get; set; }
    string DbUser { get; set; }
    string DbPassword { get; set; }
}

public class ApplicationConfig : IApplicationConfig, IApplicationConfigBase
{
    public string DbName { get; set; }
    public string DbPort { get; set; }
    public string DbUser { get; set; }
    public string DbPassword { get; set; }
}