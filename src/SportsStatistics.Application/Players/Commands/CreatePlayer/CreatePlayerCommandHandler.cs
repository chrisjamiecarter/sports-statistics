using SportsStatistics.Common.Abstractions.Messaging;
using SportsStatistics.Common.Primitives.Results;
using SportsStatistics.Domain.Entities;

namespace SportsStatistics.Application.Players.Commands.CreatePlayer;

internal sealed class CreatePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<CreatePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
    {
        var player = new Player(Guid.CreateVersion7(),
                                command.Name,
                                command.Role,
                                command.SquadNumber,
                                command.Nationality,
                                command.DateOfBirth);

        var created = await _repository.CreateAsync(player, cancellationToken);

        return created
            ? Result.Success()
            : Result.Failure(new("Player.Create", "Unable to create player."));
    }
}
