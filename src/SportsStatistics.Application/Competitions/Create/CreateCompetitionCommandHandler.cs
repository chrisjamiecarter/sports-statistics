using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Create;

internal sealed class CreateCompetitionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var seasonId = EntityId.Create(request.SeasonId);

        var season = await _dbContext.Seasons.FindAsync([seasonId], cancellationToken);
        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(seasonId));
        }

        var competition = Competition.Create(seasonId, request.Name, request.CompetitionTypeName);

        _dbContext.Competitions.Add(competition);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotCreated(competition.Id));
    }
}
