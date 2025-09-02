using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Web.Contracts.Requests;

namespace SportsStatistics.Web.Services;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{
    public const string IdentityRoutePrefix = "/api/authentication";

    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var loggerFactory = builder.ServiceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(nameof(IEndpointConventionBuilder));

        var accountGroup = builder.MapGroup(IdentityRoutePrefix);

        // Configure signin endpoint.
        accountGroup.MapPost("signin", async (
            SignInRequest request,
            IAuthenticationService authenticationService,
            CancellationToken cancellationToken) =>
        {
            // Validate required authentication parameters.
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Results.BadRequest("Email and password are required.");
            }

            // Attempt password sign-in.
            var signInResult = await authenticationService.PasswordSignInAsync(request.Email, request.Password, request.IsPersistant);
            if (signInResult.IsFailure)
            {
                return Results.BadRequest(new SignInResponse(false, null, signInResult.Error.Message));
            }

            var decodedUrl = Uri.UnescapeDataString(request.ReturnUrl ?? string.Empty);

            if (string.IsNullOrWhiteSpace(decodedUrl) || !decodedUrl.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                decodedUrl = "/"; // TODO: RedirectUrls.Home;
            }

            //return Results.Redirect(decodedUrl);
            return Results.Ok(new SignInResponse(true, decodedUrl, null));
        });

        // Configure signout endpoint.
        accountGroup.MapPost("signout", async (
            ClaimsPrincipal user,
            [FromServices] IAuthenticationService authenticationService,
            [FromForm] string? returnUrl = null) =>
        {
            await authenticationService.SignOutAsync().ConfigureAwait(false);

            logger.LogInformation("{User} has logged out.", user.Identity?.Name);

            var decodedUrl = Uri.UnescapeDataString(returnUrl ?? string.Empty);

            if (string.IsNullOrWhiteSpace(decodedUrl) || !decodedUrl.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                decodedUrl = "/"; // TODO: RedirectUrls.Home;
            }

            return TypedResults.LocalRedirect($"{decodedUrl}");
        })
        .RequireAuthorization()
        .DisableAntiforgery(); // TODO: Consider enabling antiforgery protection if needed.

        return accountGroup;
    }

    /// <summary>
    /// Validates that the requst originates from the same domain to prevent CSRF attacks.
    /// </summary>
    /// <param name="context">The HTTP context of the request.</param>
    /// <param name="logger">The logger for security warnings.</param>
    /// <returns>True is the origin is valid; otherwise false.</returns>
    private static bool ValidateRequestOrigin(HttpContext context, ILogger logger)
    {
        var referer = context.Request.Headers.Referer.ToString();
        var host = context.Request.Headers.Host.ToString();
        var scheme = context.Request.Scheme;
        var expectedOrigin = $"{scheme}://{host}";

        if (string.IsNullOrWhiteSpace(referer) || !referer.StartsWith(expectedOrigin, StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("Invalid request origin. Referer: {Referer}, Expected Origin: {ExpectedOrigin}", referer, expectedOrigin);
            return false;
        }

        return true;
    }
}
