using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetAll;

internal sealed class GetFixturesQueryHandler(IFixtureRepository repository) : IQueryHandler<GetFixturesQuery, List<FixtureResponse>>
{
    private readonly IFixtureRepository _repository = repository;

    public async Task<Result<List<FixtureResponse>>> Handle(GetFixturesQuery request, CancellationToken cancellationToken)
    {
        var fixtures = await _repository.GetAllAsync(cancellationToken);
        return fixtures.ToResponse();
    }
}
