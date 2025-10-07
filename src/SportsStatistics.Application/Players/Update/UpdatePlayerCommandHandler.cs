using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var entityId = EntityId.Create(request.Id);

        var player = await _dbContext.Players.FindAsync([entityId], cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(entityId));
        }

        var squadNumberTaken = await _dbContext.Players.AsNoTracking()
                                                       .AnyAsync(p => p.Id != player.Id && p.SquadNumber == player.SquadNumber, cancellationToken);

        if (squadNumberTaken)
        {
            return Result.Failure(PlayerErrors.SquadNumberTaken(request.SquadNumber));
        }

        player.Update(request.Name, request.SquadNumber, request.Nationality, request.DateOfBirth, request.PositionName);

        var updated = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return updated
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotUpdated(player.Id));
    }
}
