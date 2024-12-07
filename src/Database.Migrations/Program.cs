using Database.Migrations;
using Xerris.DotNet.Core;

if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOCAL_DB_HOST")))
    throw new ArgumentException("Please set env variable 'LOCAL_DB_HOST'");

Environment.SetEnvironmentVariable("stageName", "local");

var migrator = IoC.Resolve<IMigrator>();
migrator.Upgrade();