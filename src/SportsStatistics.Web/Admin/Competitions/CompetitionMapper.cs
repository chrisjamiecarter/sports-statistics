using SportsStatistics.Application.Competitions.Create;
using SportsStatistics.Application.Competitions.Delete;
using SportsStatistics.Application.Competitions.GetAll;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Web.Admin.Seasons;

namespace SportsStatistics.Web.Admin.Competitions;

internal static class CompetitionMapper
{
    public static CreateCompetitionCommand ToCreateCommand(this CompetitionFormModel competition)
        => new(competition.Season?.Id ?? default,
               competition.Name,
               competition.CompetitionType?.Name ?? string.Empty);

    public static DeleteCompetitionCommand ToDeleteCommand(this CompetitionDto competition)
        => new(competition.Id);

    public static CompetitionDto ToDto(this CompetitionResponse competition)
        => new(competition.Id,
               competition.SeasonId,
               competition.Name,
               competition.CompetitionType);

    public static CompetitionTypeOptionDto ToDto(this CompetitionType competitionType)
        => new(competitionType.Id, competitionType.Name);

    public static CompetitionFormModel ToFormModel(this CompetitionDto competition, IEnumerable<SeasonDto> seasons, IEnumerable<CompetitionTypeOptionDto> competitionTypeOptions)
        => new()
        {
            Season = seasons.SingleOrDefault(s => s.Id == competition.SeasonId),
            Name = competition.Name,
            CompetitionType = competitionTypeOptions.SingleOrDefault(t => string.Equals(t.Name, competition.CompetitionType, StringComparison.OrdinalIgnoreCase)),
        };

    public static IQueryable<CompetitionDto> ToQueryable(this List<CompetitionResponse> competitions)
        => competitions.Select(ToDto).AsQueryable();

    public static UpdateCompetitionCommand ToUpdateCommand(this CompetitionFormModel competition, Guid id)
        => new(id,
               competition.Name,
               competition.CompetitionType?.Name ?? string.Empty);
}
