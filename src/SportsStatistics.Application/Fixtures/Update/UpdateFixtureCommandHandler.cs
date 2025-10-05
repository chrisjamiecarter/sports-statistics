using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Application.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandHandler(IFixtureRepository repository, ICompetitionRepository competitionRepository) : ICommandHandler<UpdateFixtureCommand>
{
    private readonly IFixtureRepository _repository = repository;
    private readonly ICompetitionRepository _competitionRepository = competitionRepository;

    public async Task<Result> Handle(UpdateFixtureCommand request, CancellationToken cancellationToken)
    {
        var id = EntityId.Create(request.Id);

        var fixture = await _repository.GetByIdAsync(id, cancellationToken);
        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(id));
        }

        // TODO: Replace Competition with CompetitionId in Fixture entity.
        var competitionId = EntityId.Create(request.CompetitionId);
        var competition = await _competitionRepository.GetByIdAsync(competitionId, cancellationToken);
        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(competitionId));
        }

        var location = FixtureLocation.FromName(request.LocationName);

        var status = FixtureStatus.FromName(request.StatusName);

        FixtureScore? score = null;
        if (status == FixtureStatus.Completed)
        {
            score = FixtureScore.Create(request.HomeGoals, request.AwayGoals);
        }

        fixture.Update(request.KickoffTimeUtc, competition, location, score, status);

        var updated = await _repository.UpdateAsync(fixture, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotUpdated(id));
    }
}
