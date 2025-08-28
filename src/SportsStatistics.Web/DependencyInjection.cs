using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.FluentUI.AspNetCore.Components;
using SportsStatistics.Infrastructure;
using SportsStatistics.Shared.Security;
using SportsStatistics.Web.Components;
using SportsStatistics.Web.Services;

namespace SportsStatistics.Web;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddProjectDependencies(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

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

        return builder;
    }

    public static WebApplication AddProjectMiddleware(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapAdditionalIdentityEndpoints();

        return app;
    }
}
