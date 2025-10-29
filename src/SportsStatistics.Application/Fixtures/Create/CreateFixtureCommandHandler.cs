using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateFixtureCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateFixtureCommand request, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions.AsNoTracking()
                                                       .Where(competition => competition.Id == request.CompetitionId)
                                                       .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(request.CompetitionId));
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

        var fixture = Fixture.Create(EntityId.Create(request.CompetitionId), request.Opponent, request.KickoffTimeUtc, request.FixtureLocationName);

        _dbContext.Fixtures.Add(fixture);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotCreated(fixture.Opponent, fixture.KickoffTimeUtc, fixture.Location.Name));
    }
}
