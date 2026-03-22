using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Web.Reports.Models;

public sealed record FixtureDto(
    Guid FixtureId,
    Guid CompetitionId,
    string CompetitionName,
    string Opponent,
    DateTime KickoffTimeUtc,
    Location Location,
    Score Score,
    Status Status,
    Outcome Outcome);
