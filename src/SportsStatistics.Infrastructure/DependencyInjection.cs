using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Application.Seasons;
using SportsStatistics.Aspire.Constants;
using SportsStatistics.Authorization;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Competitions;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Players;
using SportsStatistics.Infrastructure.Seasons;
using SportsStatistics.Infrastructure.Services;

namespace SportsStatistics.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.AddSqlServerDbContext<ApplicationDbContext>(Resources.SqlDatabase, configureDbContextOptions: options =>
        {
            options.UseSqlServer(sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(Schemas.MigrationsHistory.Table, Schemas.MigrationsHistory.Schema);
            });
        });

        builder.AddAuthorizationInternal();

        builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        builder.Services.AddScoped<IPlayerService, PlayerService>();
        builder.Services.AddRepositories();

        builder.Services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
        builder.Services.AddScoped<IDatabaseSeederService, DatabaseSeederService>();

        return builder;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompetitionRepository, CompetitionRepository>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();

        return services;
    }
}
