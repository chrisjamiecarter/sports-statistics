using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Create;

internal sealed class CreatePlayerCommandHandler(IApplicationDbContext dbContext,
                                                 IPlayerService service) : ICommandHandler<CreatePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IPlayerService _service = service;

    public async Task<Result> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = Player.Create(request.Name,
                                   request.SquadNumber,
                                   request.Nationality,
                                   request.DateOfBirth,
                                   request.PositionName);

        var isSquadNumberAvailable = await _service.IsSquadNumberAvailableAsync(player.Id, player.SquadNumber, cancellationToken);

        if (!isSquadNumberAvailable)
        {
            return Result.Failure(PlayerErrors.SquadNumberNotAvailable(request.SquadNumber));
        }

        _dbContext.Players.Add(player);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created 
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotCreated(request.Name, request.DateOfBirth));
    }
}
