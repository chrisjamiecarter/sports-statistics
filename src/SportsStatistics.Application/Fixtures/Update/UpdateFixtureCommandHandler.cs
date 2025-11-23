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
        var fixture = await _dbContext.Fixtures.Where(fixture => fixture.Id == request.FixtureId)
                                               .SingleOrDefaultAsync(cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        var opponentResult = Opponent.Create(request.Opponent);
        var kickOffTimeUtcResult = KickoffTimeUtc.Create(request.KickoffTimeUtc);
        var locationResult = Location.Resolve(request.LocationId);
        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(opponentResult,
                                                                 kickOffTimeUtcResult,
                                                                 locationResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
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

        if (kickoffDate < season.DateRange.StartDate || kickoffDate > season.DateRange.EndDate)
        {
            return Result.Failure(FixtureErrors.KickoffTimeOutsideSeason(request.KickoffTimeUtc, season.DateRange.StartDate, season.DateRange.EndDate));
        }

        if (await _dbContext.Fixtures.AsNoTracking()
                                     .Where(fixture => fixture.Id != request.FixtureId && DateOnly.FromDateTime(fixture.KickoffTimeUtc) == kickoffDate)
                                     .AnyAsync(cancellationToken))
        {
            return Result.Failure(FixtureErrors.AlreadyScheduledOnDate(kickoffDate));
        }

        fixture.ChangeOpponent(opponentResult.Value);
        
        fixture.ChangeKickoffTimeUtc(kickOffTimeUtcResult.Value);
        
        fixture.ChangeLocation(locationResult.Value);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
