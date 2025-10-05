using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Delete;

internal sealed class DeleteFixtureCommandHandler(IFixtureRepository repository) : ICommandHandler<DeleteFixtureCommand>
{
    private readonly IFixtureRepository _repository = repository;

    public async Task<Result> Handle(DeleteFixtureCommand request, CancellationToken cancellationToken)
    {
        var id = EntityId.Create(request.Id);

        var competition = await _repository.GetByIdAsync(id, cancellationToken);
        if (competition is null)
        {
            return Result.Failure(FixtureErrors.NotFound(id));
        }

        var deleted = await _repository.DeleteAsync(competition, cancellationToken);

        return deleted
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotDeleted(id));
    }
}
