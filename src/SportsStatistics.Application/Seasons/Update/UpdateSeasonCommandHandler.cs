using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Update;

internal sealed class UpdateSeasonCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateSeasonCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = await _dbContext.Seasons.Where(season => season.Id == request.SeasonId)
                                             .SingleOrDefaultAsync(cancellationToken);

        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(request.SeasonId));
        }

        season.Update(request.StartDate, request.EndDate);

        var updated = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return updated
            ? Result.Success()
            : Result.Failure(SeasonErrors.NotUpdated(request.SeasonId));
    }
}
