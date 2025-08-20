using SportsStatistics.Web.Components;

namespace SportsStatistics.Web;

public static class WebServiceRegistration
{
    public static IHostApplicationBuilder AddWebDependencies(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        return builder;
    }

    public static WebApplication AddWebMiddleware(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
