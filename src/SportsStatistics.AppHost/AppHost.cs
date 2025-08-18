using Projects;
using SportsStatistics.Shared.Aspire;

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
        
        builder.AddProject<SportsStatistics_Web>(ProjectResourceConstants.Web)
               .WithReference(database)
               .WaitFor(database);

        await builder.Build().RunAsync();
    }
}