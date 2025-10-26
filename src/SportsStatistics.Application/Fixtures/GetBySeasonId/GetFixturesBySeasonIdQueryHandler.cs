using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

internal sealed class GetFixturesBySeasonIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetFixturesBySeasonIdQuery, List<FixtureResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<FixtureResponse>>> Handle(GetFixturesBySeasonIdQuery request, CancellationToken cancellationToken)
    {
        var seasonId = EntityId.Create(request.SeasonId);

        return await _dbContext.Fixtures
            .AsNoTracking()
            .Join(_dbContext.Competitions.AsNoTracking().Where(competition => competition.SeasonId == seasonId),
                  fixture => fixture.CompetitionId,
                  competition => competition.Id,
                  (fixture, competition) => fixture.ToResponse(competition))
            .ToListAsync(cancellationToken);
    }
}
