using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var entityId = EntityId.Create(request.Id);

        var player = await _repository.GetByIdAsync(entityId, cancellationToken);
        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(entityId));
        }

        var position = Position.FromName(request.Position);
        if (position == Position.Unknown)
        {
            return Result.Failure(PlayerErrors.InvalidPosition(request.Position));
        }

        player.Update(request.Name, request.SquadNumber, request.Nationality, request.DateOfBirth, position);

        var updated = await _repository.UpdateAsync(player, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotUpdated(player.Id));
    }
}
