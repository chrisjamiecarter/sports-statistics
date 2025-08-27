using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Infrastructure.Persistence;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Infrastructure.Persistence.Services;
using SportsStatistics.Shared.Aspire;

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
                sqlOptions.MigrationsHistoryTable("MigrationsHistory", "efcore");
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

        return builder;
    }
}
