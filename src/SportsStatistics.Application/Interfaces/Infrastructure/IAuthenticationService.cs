using SportsStatistics.Application.Models;
using SportsStatistics.Core.Results;

namespace SportsStatistics.Application.Interfaces.Infrastructure;

public interface IAuthenticationService
{
    Task<ApplicationUserDto?> GetCurrentUserAsync();
    Task<Result> SignInAsync(string email, string password, bool isPersistant);
    Task SignOutAsync();
}
