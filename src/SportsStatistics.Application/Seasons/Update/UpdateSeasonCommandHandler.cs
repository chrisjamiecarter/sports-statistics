using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Update;

internal sealed class UpdateSeasonCommandHandler(ISeasonRepository repository) : ICommandHandler<UpdateSeasonCommand>
{
    private readonly ISeasonRepository _repository = repository;

    public async Task<Result> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
    {
        var idResult = EntityId.Create(request.Id);
        if (idResult.IsFailure)
        {
            return idResult;
        }

        var id = idResult.Value;

        var season = await _repository.GetByIdAsync(id, cancellationToken);
        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(id));
        }

        season.Update(request.StartDate, request.EndDate);

        var updated = await _repository.UpdateAsync(season, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(SeasonErrors.NotUpdated(id));
    }
}
