using Projects;
using SportsStatistics.Aspire.Constants;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var sqlServer = builder.AddSqlServer(Resources.SqlName, port: Resources.SqlPort)
                               .WithContainerName(Resources.SqlContainerName)
                               .WithLifetime(ContainerLifetime.Persistent);

        var database = sqlServer.AddDatabase(Resources.SqlDatabase);

        var migrator = builder.AddProject<DatabaseMigratorProject>(Resources.DatabaseMigrator)
                              .WithReference(database)
                              .WaitFor(database);

        var web = builder.AddProject<WebProject>(Resources.Web)
                         .WithReference(database)
                         .WaitFor(migrator);

        await builder.Build().RunAsync();
    }
}
