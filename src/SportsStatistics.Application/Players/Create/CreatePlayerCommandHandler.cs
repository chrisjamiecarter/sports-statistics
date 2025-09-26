using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Create;

internal sealed class CreatePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<CreatePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var position = Position.FromName(request.Position);
        if (position == Position.Unknown)
        {
            return Result.Failure(PlayerErrors.InvalidPosition(request.Position));
        }

        var player = Player.Create(request.Name, request.SquadNumber, request.Nationality, request.DateOfBirth, position);

        var created = await _repository.CreateAsync(player, cancellationToken);

        return created
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotCreated(player.Id));
    }
}
