using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Web.Contracts.Requests;

namespace SportsStatistics.Web.Api.Endpoints.Identity;

internal static class SigninEndpoint
{
    public const string Name = "IdentitySignin";

    public static IEndpointRouteBuilder MapSigninEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Routes.Identity.SignIn,
            async (SignInRequest request,
                   IAuthenticationService authenticationService,
                   CancellationToken cancellationToken) =>
            {
                // Validate required authentication parameters.
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return Results.BadRequest("Email and password are required.");
                }

                // Attempt password sign-in.
                var result = await authenticationService.PasswordSignInAsync(request.Email, request.Password, request.IsPersistant);
                if (result.IsFailure)
                {
                    return Results.BadRequest(new SignInResponse(false, result.Error.Message));
                }

                return Results.Ok(new SignInResponse(true, "User signed in successfully."));
            })
            .WithName(Name)
            .Produces<SignInResponse>(StatusCodes.Status200OK)
            .Produces<SignInResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status400BadRequest);
     
        return builder;
    }
}
