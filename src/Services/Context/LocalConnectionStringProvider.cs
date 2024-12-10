using Xerris.DotNet.Data;

namespace Services.Context;

public interface IConnectionBuilder
{
    string AdminConnectionString { get; }
}

public class LocalConnectionStringProvider(IApplicationConfig config) : IConnectionStringProvider
{
    public string ConnectionString => config.LocalDbConfig.ConnectionString;
}