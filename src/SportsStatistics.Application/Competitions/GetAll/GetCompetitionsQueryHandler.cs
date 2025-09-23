using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.GetAll;

internal sealed class GetCompetitionsQueryHandler(ICompetitionRepository repository) : IQueryHandler<GetCompetitionsQuery, List<CompetitionResponse>>
{
    private readonly ICompetitionRepository _repository = repository;

    public async Task<Result<List<CompetitionResponse>>> Handle(GetCompetitionsQuery request, CancellationToken cancellationToken)
    {
        var competitions = await _repository.GetAllAsync(cancellationToken);
        return competitions.ToResponse();
    }
}
