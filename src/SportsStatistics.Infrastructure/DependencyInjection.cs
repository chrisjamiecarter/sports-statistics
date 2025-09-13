using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Application.Players;
using SportsStatistics.Core.Aspire;
using SportsStatistics.Infrastructure.Identity.Providers;
using SportsStatistics.Infrastructure.Persistence;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Infrastructure.Persistence.Players;
using SportsStatistics.Infrastructure.Persistence.Schemas;
using SportsStatistics.Infrastructure.Persistence.Services;

namespace SportsStatistics.Infrastructure;

public static class DependencyInjection
{
    public enum SourceProject
    {
        Unknown = 0,
        DatabaseMigrator,
        WebApplication
    }

    public static IHostApplicationBuilder AddInfrastructureDependencies(this IHostApplicationBuilder builder, SourceProject project)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        switch (project)
        {
            case SourceProject.DatabaseMigrator:
                AddDatabaseMigratorDependencies(builder);
                break;
            case SourceProject.WebApplication:
                AddWebApplicationDependencies(builder);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(project));
        }

        return builder;
    }

    private static IHostApplicationBuilder AddDatabaseMigratorDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<SportsStatisticsDbContext>(SqlResourceConstants.Database, configureDbContextOptions: options =>
        {
            options.UseSqlServer(sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(EntityFrameworkCoreSchema.MigrationsHistory.Table, EntityFrameworkCoreSchema.MigrationsHistory.Schema);
            });
        });

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<SportsStatisticsDbContext>();

        builder.Services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
        builder.Services.AddScoped<IDatabaseSeederService, DatabaseSeederService>();

        return builder;
    }

    private static IHostApplicationBuilder AddWebApplicationDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<SportsStatisticsDbContext>(SqlResourceConstants.Database);

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<SportsStatisticsDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        //builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingServerAuthenticationStateProvider>();

        builder.Services.AddRepositories();

        return builder;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IPlayerRepository, PlayerRepository>();

        return services;
    }
}
