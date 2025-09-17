using FluentValidation;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Create;

internal sealed class CreatePlayerCommandHandler(IPlayerRepository repository) : ICommandHandler<CreatePlayerCommand>
{
    private readonly IPlayerRepository _repository = repository;

    public async Task<Result> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
    {
        var position = Position.FromName(command.Position);

        var player = new Player
        {
            Id = Guid.CreateVersion7(),
            Name = command.Name,
            SquadNumber = command.SquadNumber,
            Nationality = command.Nationality,
            DateOfBirth = command.DateOfBirth,
            Position = position,
        };

        var created = await _repository.CreateAsync(player, cancellationToken);

        return created
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotCreated(player.Id));
    }
}
