using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandHandler(IFixtureRepository repository) : ICommandHandler<UpdateFixtureCommand>
{
    private readonly IFixtureRepository _repository = repository;

    public async Task<Result> Handle(UpdateFixtureCommand request, CancellationToken cancellationToken)
    {
        var id = EntityId.Create(request.Id);

        var fixture = await _repository.GetByIdAsync(id, cancellationToken);
        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(id));
        }

        var location = FixtureLocation.FromName(request.LocationName);

        fixture.Update(request.Opponent, request.KickoffTimeUtc, location);

        var updated = await _repository.UpdateAsync(fixture, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotUpdated(id));
    }
}
