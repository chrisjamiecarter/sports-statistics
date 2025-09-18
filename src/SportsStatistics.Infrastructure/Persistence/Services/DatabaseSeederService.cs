using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Authorization.Constants;
using SportsStatistics.Authorization.Entities;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Persistence.Services;

internal sealed class DatabaseSeederService : IDatabaseSeederService
{
    private readonly SportsStatisticsDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<DatabaseSeederService> _logger;

    public DatabaseSeederService(SportsStatisticsDbContext dbContext,
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ILogger<DatabaseSeederService> logger)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<Result> SeedAsync(CancellationToken cancellationToken)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            var rolesResult = await SeedRolesAsync();
            if (rolesResult.IsFailure)
            {
                return rolesResult;
            }

            var usersResult = await SeedAdminUserAsync();
            if (usersResult.IsFailure)
            {
                return usersResult;
            }

            return Result.Success();
        });
    }

    private async Task<Result> SeedAdminUserAsync()
    {
        var adminUsername = "admin";
        var adminPassword = "Admin123#";
        var adminEmail = "admin@sportsstatistics.com";
        var adminRole = Roles.Administrator;

        var adminExists = await _userManager.FindByEmailAsync(adminEmail) is not null || await _userManager.FindByNameAsync(adminUsername) is not null;
        if (!adminExists)
        {
            var adminUser = new ApplicationUser
            {
                Email = adminEmail,
                EmailConfirmed = true,
                UserName = adminUsername,
            };

            var createResult = await _userManager.CreateAsync(adminUser, adminPassword);
            if (!createResult.Succeeded)
            {
                // TODO:
                return Result.Failure(Error.Failure("User.Create", $"Failed to create user '{adminUsername}': {string.Join(", ", createResult.Errors.Select(e => e.Description))}"));
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, adminRole);
            if (!addToRoleResult.Succeeded)
            {
                // TODO:
                return Result.Failure(Error.Failure("User.AddToRole", $"Failed to add user '{adminUsername}' to role '{adminRole}': {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}"));
            }
        }

        return Result.Success();
    }

    private async Task<Result> SeedRolesAsync()
    {
        var roleNames = typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                             .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                             .Select(f => f.GetRawConstantValue()?.ToString())
                             .Where(roleName => !string.IsNullOrWhiteSpace(roleName))
                             .ToList();

        foreach (var roleName in roleNames)
        {
            var result = await SeedRoleAsync(roleName!);
            if (result.IsFailure)
            {
                return result;
            }
        }

        return Result.Success();
    }

    private async Task<Result> SeedRoleAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                // TODO:
                return Result.Failure(Error.Failure("Role.Create", $"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}"));
            }
        }

        return Result.Success();
    }
}
