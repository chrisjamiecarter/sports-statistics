using Microsoft.EntityFrameworkCore;
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
        var season = await _dbContext.Seasons.AsNoTracking()
                                             .Where(season => season.Id == request.SeasonId)
                                             .SingleOrDefaultAsync(cancellationToken);

        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(request.SeasonId));
        }

        var competition = Competition.Create(EntityId.Create(request.SeasonId), request.Name, request.CompetitionTypeName);

        _dbContext.Competitions.Add(competition);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotCreated(request.Name, request.CompetitionTypeName));
    }
}
