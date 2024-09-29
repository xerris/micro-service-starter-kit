using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Xerris.DotNet.Core;

namespace Skillz.Database.Migrations;

public interface IMigrator
{
    void Upgrade();
    void Downgrade(int version);
}

public class Migrator : IMigrator
{
    public void Upgrade()
    {
        using var scope = IoC.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    public void Downgrade(int version)
    {
        using var scope = IoC.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(version);
    }
}