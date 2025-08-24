namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddProjectDependencies();

        var host = builder.Build();
        host.AddProjectMiddleware();

        await host.RunAsync();
    }
}