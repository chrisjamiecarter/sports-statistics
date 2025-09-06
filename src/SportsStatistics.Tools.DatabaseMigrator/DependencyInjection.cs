using SportsStatistics.Infrastructure;
using static SportsStatistics.Infrastructure.DependencyInjection;

namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddProjectDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.AddInfrastructureDependencies(SourceProject.DatabaseMigrator);

        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry()
                        .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        return builder;
    }

    public static IHost AddProjectMiddleware(this IHost host)
    {
        ArgumentNullException.ThrowIfNull(host, nameof(host));

        return host;
    }
}
