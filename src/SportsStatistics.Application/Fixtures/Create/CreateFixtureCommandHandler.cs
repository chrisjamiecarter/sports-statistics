using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateFixtureCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateFixtureCommand request, CancellationToken cancellationToken)
    {
        var competitionId = EntityId.Create(request.CompetitionId);

        var competition = await _dbContext.Competitions.AsNoTracking()
                                                       .SingleOrDefaultAsync(c => c.Id == competitionId, cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(competitionId));
        }

        var fixture = Fixture.Create(competitionId, request.Opponent, request.KickoffTimeUtc, request.FixtureLocationName);

        _dbContext.Fixtures.Add(fixture);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotCreated(fixture.Id));
    }
}
