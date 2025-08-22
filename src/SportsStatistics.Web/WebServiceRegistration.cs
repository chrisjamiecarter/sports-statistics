using SportsStatistics.Shared.Security;
using SportsStatistics.Web.Components;

namespace SportsStatistics.Web;

internal static class WebServiceRegistration
{
    public static IHostApplicationBuilder AddWebDependencies(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorizationBuilder()
                        .AddPolicy(Policies.RequireAdministratorRole, policy => policy.RequireRole([Roles.Administrator]));

        return builder;
    }

    public static WebApplication AddWebMiddleware(this WebApplication app)
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

        return app;
    }
}
