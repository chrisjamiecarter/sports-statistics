namespace SportsStatistics.Web.Reports.Models;

public sealed record SeasonDto(
    Guid SeasonId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Name);
