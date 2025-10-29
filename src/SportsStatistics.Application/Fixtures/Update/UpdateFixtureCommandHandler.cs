using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateFixtureCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdateFixtureCommand request, CancellationToken cancellationToken)
    {
        var fixture = await _dbContext.Fixtures.AsNoTracking()
                                               .Where(fixture => fixture.Id == request.Id)
                                               .SingleOrDefaultAsync(cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(request.Id));
        }

        var competition = await _dbContext.Competitions.AsNoTracking()
                                                       .Where(competition => competition.Id == fixture.CompetitionId)
                                                       .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(fixture.CompetitionId));
        }

        var season = await _dbContext.Seasons.AsNoTracking()
                                             .Where(season => season.Id == competition.SeasonId)
                                             .SingleOrDefaultAsync(cancellationToken);

        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(competition.SeasonId));
        }

        var kickoffDate = DateOnly.FromDateTime(request.KickoffTimeUtc);

        if (kickoffDate < season.StartDate || kickoffDate > season.EndDate)
        {
            return Result.Failure(FixtureErrors.KickoffTimeOutsideSeason(request.KickoffTimeUtc, season.StartDate, season.EndDate));
        }

        if (!fixture.Update(request.Opponent, request.KickoffTimeUtc, request.FixtureLocationName))
        {
            return Result.Success();
        }

        var updated = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return updated
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotUpdated(fixture.Id));
    }
}
