using SportsStatistics.Application.Clubs.Update;

namespace SportsStatistics.Web.Admin.Clubs;

public sealed class ClubFormModel
{
    public string Name { get; set; } = string.Empty;
    
    public UpdateClubCommand ToUpdateCommand(Guid id) => new(id, Name.Trim());
}
