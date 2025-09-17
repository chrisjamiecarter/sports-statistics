using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Delete;

internal sealed class DeletePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<DeletePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.Id));
        }

        var deleted = await _repository.DeleteAsync(player, cancellationToken);
        return deleted
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotDeleted(player.Id));
    }
}
