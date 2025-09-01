using SportsStatistics.Web;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddSportsStatisticsWeb();

        var app = builder.Build();
        app.ConfigureSportsStatisticsWeb();

        await app.RunAsync();
    }
}