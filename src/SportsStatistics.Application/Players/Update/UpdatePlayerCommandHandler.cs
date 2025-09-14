using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (player is null)
        {
            return Result.Failure(Error.NotFound("Player.Update", "Player not found."));
        }

        player.Update(request.Name, request.Role, request.SquadNumber, request.Nationality, request.DateOfBirth);

        var updated = await _repository.UpdateAsync(player, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(Error.Failure("Player.Update", "Unable to update player."));
    }
}
