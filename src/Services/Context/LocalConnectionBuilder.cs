using System.Data;

namespace Services.Context;

public interface IConnectionBuilder
{
    string AdminConnectionString { get; }
}

public class LocalConnectionBuilder : IConnectionBuilder
{
    public const string Server = "127.0.0.1";
    private readonly IApplicationConfig appConfig;

    public LocalConnectionBuilder(IApplicationConfig appConfig)
    {
        this.appConfig = appConfig;
    }

    public string AdminConnectionString =>
        $"Server={Server};Port={appConfig.DbPort};Database={appConfig.DbName};User Id={appConfig.DbUser};Password={appConfig.DbPassword};";

    public Task<IDbConnection> CreateConnectionAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDbConnection> CreateReadConnectionAsync()
    {
        throw new NotImplementedException();
    }
}