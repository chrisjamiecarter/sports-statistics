using SportsStatistics.Application.Seasons.GetAll;

namespace SportsStatistics.Web.Admin.Seasons;

internal static class SeasonDtoMapper
{
    public static SeasonDto ToDto(this SeasonResponse season)
        => new(season.Id,
               season.StartDate,
               season.EndDate,
               season.Name);

    public static IQueryable<SeasonDto> ToQueryable(this List<SeasonResponse> seasons)
    {
        return seasons.Select(ToDto).AsQueryable();
    }
}
