using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Infrastructure.Persistence;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Shared.Aspire;

namespace SportsStatistics.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IHostApplicationBuilder AddInfrastructureDependencies(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

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
        .AddEntityFrameworkStores<SportsStatisticsDbContext>();

        return builder;
    }
}
