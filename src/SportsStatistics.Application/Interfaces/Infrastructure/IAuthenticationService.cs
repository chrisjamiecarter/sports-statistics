using SportsStatistics.Application.Models;
using SportsStatistics.Core.Results;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IAuthenticationService
{
    Task<ApplicationUserDto?> GetCurrentUserAsync();
    Task<Result> CheckPasswordAsync(string email, string password);
    Task<Result> PasswordSignInAsync(string email, string password, bool isPersistant);
    Task SignOutAsync();
}
