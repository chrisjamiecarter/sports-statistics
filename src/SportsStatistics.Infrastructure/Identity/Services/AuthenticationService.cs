﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Application.Models;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Infrastructure.Persistence.Models.Mappings;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Identity.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(AuthenticationStateProvider authenticationStateProvider,
                                 SignInManager<ApplicationUser> signInManager,
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

    public async Task<Result> PasswordSignInAsync(string email, string password, bool isPersistant)
    {
        var user = await GetApplicationUserByEmailAsync(email);
        if (user == null)
        {
            return Result.Failure(Error.Failure("Authentication.InvalidSignInAttempt", "Invalid Sign In Attempt"));
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistant, false);
        return result switch
        {
            { Succeeded: true } => Result.Success(),
            { IsLockedOut: true } => Result.Failure(Error.Failure("Authentication.UserLockedOut", "User account is locked out")),
            { IsNotAllowed: true } => Result.Failure(Error.Failure("Authentication.UserNotAllowed", "User is not allowed to sign in")),
            { RequiresTwoFactor: true } => Result.Failure(Error.Failure("Authentication.TwoFactorRequired", "Two-factor authentication is required")),
            _ => Result.Failure(Error.Failure("Authentication.InvalidSignInAttempt", "Invalid Sign In Attempt")),
        };
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<ApplicationUser?> GetApplicationUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    private async Task<ApplicationUser?> GetApplicationUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    private async Task<ApplicationUser?> GetApplicationUserByUserNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
}
