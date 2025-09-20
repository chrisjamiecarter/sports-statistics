namespace SportsStatistics.Web.Admin.Seasons;

public sealed record SeasonDto(Guid Id,
                               DateOnly StartDate,
                               DateOnly EndDate,
                               string Name);
