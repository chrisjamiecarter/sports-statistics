using SportsStatistics.Infrastructure;
using SportsStatistics.Web;
using SportsStatistics.Web.Components;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.AddInfrastructureDependencies();
        builder.AddWebDependencies();

        var app = builder.Build();

        app.AddWebMiddleware();

        app.Run();
    }
}