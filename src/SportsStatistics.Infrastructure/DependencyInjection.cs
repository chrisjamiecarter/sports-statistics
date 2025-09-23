using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Competitions;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Application.Players;
using SportsStatistics.Application.Seasons;
using SportsStatistics.Aspire.Constants;
using SportsStatistics.Authorization;
using SportsStatistics.Infrastructure.Persistence;
using SportsStatistics.Infrastructure.Persistence.Competitions;
using SportsStatistics.Infrastructure.Persistence.Players;
using SportsStatistics.Infrastructure.Persistence.Schemas;
using SportsStatistics.Infrastructure.Persistence.Seasons;
using SportsStatistics.Infrastructure.Persistence.Services;

namespace SportsStatistics.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.AddSqlServerDbContext<SportsStatisticsDbContext>(Resources.SqlDatabase, configureDbContextOptions: options =>
        {
            options.UseSqlServer(sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(SportsStatisticsSchema.MigrationsHistory.Table, SportsStatisticsSchema.MigrationsHistory.Schema);
            });
        });

        builder.AddAuthorizationInternal();

        builder.Services.AddRepositories();

        builder.Services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
        builder.Services.AddScoped<IDatabaseSeederService, DatabaseSeederService>();

        return builder;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompetitionRepository, CompetitionRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();

        return services;
    }
}
