using Projects;
using SportsStatistics.Core.Aspire;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var sqlServer = builder.AddSqlServer(SqlResourceConstants.Name)
                               .WithHostPort(SqlResourceConstants.Port)
                               .WithLifetime(ContainerLifetime.Persistent)
                               .WithContainerName(SqlResourceConstants.ContainerName);

        var database = sqlServer.AddDatabase(SqlResourceConstants.Database);
        
        var migrator = builder.AddProject<SportsStatistics_Tools_DatabaseMigrator>(ProjectResourceConstants.DatabaseMigrator)
                              .WithReference(database)
                              .WaitFor(database);

        var web = builder.AddProject<SportsStatistics_Web>(ProjectResourceConstants.Web)
                         .WithReference(database)
                         .WaitFor(migrator);

        await builder.Build().RunAsync();
    }
}