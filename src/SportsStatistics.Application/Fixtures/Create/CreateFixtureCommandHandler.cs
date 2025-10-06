using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Application.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandHandler(IFixtureRepository repository,
                                                  ICompetitionRepository competitionRepository) : ICommandHandler<CreateFixtureCommand>
{
    private readonly IFixtureRepository _repository = repository;
    private readonly ICompetitionRepository _competitionRepository = competitionRepository;

    public async Task<Result> Handle(CreateFixtureCommand request, CancellationToken cancellationToken)
    {
        var competitionId = EntityId.Create(request.CompetitionId);

        var competition = await _competitionRepository.GetByIdAsync(competitionId, cancellationToken);
        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(competitionId));
        }

        var location = FixtureLocation.FromName(request.FixtureLocation);
        if (location == FixtureLocation.Unknown)
        {
            return Result.Failure(FixtureErrors.InvalidLocation(request.FixtureLocation));
        }

        var fixture = Fixture.Create(competitionId, request.KickoffTimeUtc, location);

        var created = await _repository.CreateAsync(fixture, cancellationToken);

        return created
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotCreated(fixture.Id));
    }
}
