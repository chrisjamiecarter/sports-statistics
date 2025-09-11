using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.FluentUI.AspNetCore.Components;
using SportsStatistics.Application;
using SportsStatistics.Core.Security;
using SportsStatistics.Infrastructure;
using SportsStatistics.Web.Api.Endpoints;
using SportsStatistics.Web.Components;
using SportsStatistics.Web.Services;

namespace SportsStatistics.Web;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddSportsStatisticsWeb(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.AddApplicationDependencies();
        builder.AddInfrastructureDependencies(Infrastructure.DependencyInjection.SourceProject.WebApplication);

        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        //builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectService>();
        //builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddHttpClient();
        builder.Services.AddFluentUIComponents();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddIdentityCookies();

        builder.Services.AddAuthorizationBuilder()
                        .AddPolicy(Policies.RequireAdministratorRole, policy => policy.RequireRole([Roles.Administrator]));

        builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        builder.Services.AddScoped<ISenderService, SenderService>();

        return builder;
    }

    public static WebApplication ConfigureSportsStatisticsWeb(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapApiEndpoints();

        return app;
    }
}
