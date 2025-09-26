using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Delete;

internal sealed class DeleteSeasonCommandHandler(ISeasonRepository repository) : ICommandHandler<DeleteSeasonCommand>
{
    private readonly ISeasonRepository _repository = repository;

    public async Task<Result> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
    {
        var id = EntityId.Create(request.Id);

        var season = await _repository.GetByIdAsync(id, cancellationToken);
        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(id));
        }

        var deleted = await _repository.DeleteAsync(season, cancellationToken);

        return deleted
            ? Result.Success()
            : Result.Failure(SeasonErrors.NotDeleted(id));
    }
}
