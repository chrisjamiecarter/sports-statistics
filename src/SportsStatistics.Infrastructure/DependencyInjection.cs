using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Aspire.Constants;
using SportsStatistics.Authorization;
using SportsStatistics.Infrastructure.Database;
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

        builder.Services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
        builder.Services.AddScoped<IDatabaseSeederService, DatabaseSeederService>();

        return builder;
    }
}
