using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Seasons.GetById;

internal static class SeasonMapper
{
    public static SeasonResponse ToResponse(this Season season)
        => new(season.Id.Value,
               season.StartDate,
               season.EndDate,
               season.Name);
}
