using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandHandler(IApplicationDbContext dbContext,
                                                 IPlayerService service) : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IPlayerService _service = service;

    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var entityId = EntityId.Create(request.Id);

        var player = await _dbContext.Players.FindAsync([entityId], cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(entityId));
        }

        var isSquadNumberAvailable = await _service.IsSquadNumberAvailableAsync(player.Id, player.SquadNumber, cancellationToken);

        if (!isSquadNumberAvailable)
        {
            return Result.Failure(PlayerErrors.SquadNumberNotAvailable(request.SquadNumber));
        }

        player.Update(request.Name, request.SquadNumber, request.Nationality, request.DateOfBirth, request.PositionName);

        var updated = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return updated
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotUpdated(player.Id));
    }
}
