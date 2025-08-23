namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddDatabaseMigratorDependencies();

        var host = builder.Build();
        await host.RunAsync();
    }
}