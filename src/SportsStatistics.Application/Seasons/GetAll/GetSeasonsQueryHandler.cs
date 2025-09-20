using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.GetAll;

internal sealed class GetSeasonsQueryHandler(ISeasonRepository repository) : IQueryHandler<GetSeasonsQuery, List<SeasonResponse>>
{
    private readonly ISeasonRepository _repository = repository;

    public async Task<Result<List<SeasonResponse>>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
    {
        var seasons = await _repository.GetAllAsync(cancellationToken);
        return seasons.ToResponse();
    }
}
