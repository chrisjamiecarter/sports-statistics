using SportsStatistics.Common.Abstractions.Messaging;
using SportsStatistics.Common.Primitives.Results;

namespace SportsStatistics.Application.Players.Commands.UpdatePlayer;

internal sealed class UpdatePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (player is null)
        {
            return Result.Failure(new("Player.Update", "Player not found."));
        }

        player.Update(request.Name, request.Role, request.SquadNumber, request.Nationality, request.DateOfBirth);

        var updated = await _repository.UpdateAsync(player, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(new("Player.Update", "Unable to update player."));
    }
}
