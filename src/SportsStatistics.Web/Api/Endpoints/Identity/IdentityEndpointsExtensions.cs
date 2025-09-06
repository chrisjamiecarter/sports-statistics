namespace SportsStatistics.Web.Api.Endpoints.Identity;

internal static class IdentityEndpointsExtensions
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.MapSigninEndpoint();
        builder.MapSignoutEndpoint();

        return builder;
    }
}
