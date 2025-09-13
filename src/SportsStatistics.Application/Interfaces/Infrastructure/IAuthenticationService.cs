using SportsStatistics.Application.Models;
using SportsStatistics.Common.Primitives.Results;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IAuthenticationService
{
    Task<ApplicationUserDto?> GetCurrentUserAsync();
    Task<Result> PasswordSignInAsync(string email, string password, bool isPersistant);
    Task SignOutAsync();
}
