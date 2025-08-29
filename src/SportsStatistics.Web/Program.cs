using SportsStatistics.Web;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddProjectDependencies();

        var app = builder.Build();
        app.AddProjectMiddleware();

        await app.RunAsync();
    }
}