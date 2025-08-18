using Microsoft.Extensions.Hosting;
using SportsStatistics.Infrastructure.Persistence;
using SportsStatistics.Shared.Aspire;

namespace SportsStatistics.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IHostApplicationBuilder AddInfrastructureDependencies(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.AddSqlServerDbContext<SportsStatisticsDbContext>(SqlResourceConstants.Database);

        return builder;
    }
}
