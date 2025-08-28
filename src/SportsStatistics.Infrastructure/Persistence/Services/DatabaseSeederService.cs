using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SportsStatistics.Application.Interfaces.Infrastructure;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Shared.Results;
using SportsStatistics.Shared.Security;

namespace SportsStatistics.Infrastructure.Persistence.Services;

internal class DatabaseSeederService : IDatabaseSeederService
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

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        var adminUsername = "admin";
        var adminPassword = "Admin123#";
        var adminEmail = "admin@sportsstatistics.com";
        var adminRole = Roles.Administrator;

        var roleNames = typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                     .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                                     .Select(f => f.GetRawConstantValue()?.ToString())
                                     .Where(name => !string.IsNullOrWhiteSpace(name))
                                     .ToList();

        foreach (var roleName in roleNames)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                continue;
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var identityRole = new IdentityRole(roleName);

                var createResult = await _roleManager.CreateAsync(identityRole);
                if (!createResult.Succeeded)
                {
                    // TODO:
                    var message = $"Failed to create role '{identityRole.Name}': {string.Join(", ", createResult.Errors.Select(e => e.Description))}";
                    _logger.LogError("Failed to create role '{Role}': {ErrorsDescription}", identityRole.Name, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    return Result.Failure(new("Role.Create", message));
                }
            }
        }

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
                return Result.Failure(new("User.Create", $"Failed to create user '{adminUsername}': {string.Join(", ", createResult.Errors.Select(e => e.Description))}"));
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, adminRole);
            if (!addToRoleResult.Succeeded)
            {
                // TODO:
                return Result.Failure(new("User.AddToRole", $"Failed to add user '{adminUsername}' to role '{adminRole}': {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}"));
            }
        }

        return Result.Success();
    }
}
