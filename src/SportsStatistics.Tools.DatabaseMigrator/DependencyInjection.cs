using SportsStatistics.Infrastructure;
using SportsStatistics.Infrastructure.Persistence;
using SportsStatistics.Shared.Aspire;

namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddDatabaseMigratorDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.AddInfrastructureDependencies();

        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry()
                        .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        return builder;
    }
}
