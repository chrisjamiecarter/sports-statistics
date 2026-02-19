namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

public sealed record TeamsheetPlayerResponse(Guid Id,
                                             Guid PlayerId,
                                             string Name,
                                             int SquadNumber,
                                             string Position);
