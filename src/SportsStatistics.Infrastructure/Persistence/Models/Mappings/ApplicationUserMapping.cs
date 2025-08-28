using SportsStatistics.Application.Models;

namespace SportsStatistics.Infrastructure.Persistence.Models.Mappings;

internal static class ApplicationUserMapping
{
    public static ApplicationUserDto ToDto(this ApplicationUser user)
        => new(user.Id, user.UserName ?? string.Empty, user.Email ?? string.Empty);
}
