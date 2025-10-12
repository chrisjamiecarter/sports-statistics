using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetAll;

internal sealed class GetFixturesQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetFixturesQuery, List<FixtureResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<FixtureResponse>>> Handle(GetFixturesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Fixtures
            .Join(_dbContext.Competitions,
                  fixture => fixture.CompetitionId,
                  competition => competition.Id,
                  (fixture, competition) => fixture.ToResponse(competition))
            .ToListAsync(cancellationToken);
    }
}
