using Xerris.DotNet.Data;

namespace Services.Context;

public class LocalConnectionStringProvider(IApplicationConfig config) : IConnectionStringProvider
{
    public string ConnectionString => config.LocalDbConfig.ConnectionString;
}