using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Application.Models;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Infrastructure.Persistence.Models.Mappings;
using SportsStatistics.Shared.Results;

namespace SportsStatistics.Infrastructure.Persistence.Services;

internal class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApplicationUserDto?> GetCurrentUserAsync()
    {
        var userClaimsPrincipal = _httpContextAccessor.HttpContext?.User;
        if (userClaimsPrincipal == null)
        {
            return null;
        }

        var user = await _userManager.GetUserAsync(userClaimsPrincipal);

        return user?.ToDto();
    }

    public async Task<Result> SignInAsync(string email, string password, bool isPersistant)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistant, false);
        return result switch
        {
            { Succeeded: true } => Result.Success(),
            { IsLockedOut: true } => Result.Failure(new("Authentication.UserLockedOut", "User account is locked out")),
            { IsNotAllowed: true } => Result.Failure(new("Authentication.UserNotAllowed", "User is not allowed to sign in")),
            { RequiresTwoFactor: true } => Result.Failure(new("Authentication.TwoFactorRequired", "Two-factor authentication is required")),
            _ => Result.Failure(new("Authentication.InvalidSignInAttempt", "Invalid Sign In Attempt")),
        };
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
