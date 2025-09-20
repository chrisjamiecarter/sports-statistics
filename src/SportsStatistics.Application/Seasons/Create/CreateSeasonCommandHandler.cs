using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Create;

internal sealed class CreateSeasonCommandHandler(ISeasonRepository repository) : ICommandHandler<CreateSeasonCommand>
{
    private readonly ISeasonRepository _repository = repository;

    public async Task<Result> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = Season.Create(request.StartDate, request.EndDate);

        var created = await _repository.CreateAsync(season, cancellationToken);

        return created
            ? Result.Success()
            : Result.Failure(SeasonErrors.NotCreated(season.Id));
    }
}
