using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Create;

internal sealed class CreatePlayerCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreatePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = Player.Create(request.Name,
                                   request.SquadNumber,
                                   request.Nationality,
                                   request.DateOfBirth,
                                   request.PositionName);

        var squadNumberTaken = await _dbContext.Players.AsNoTracking()
                                                       .AnyAsync(p => p.Id != player.Id && p.SquadNumber == player.SquadNumber, cancellationToken);

        if (squadNumberTaken)
        {
            return Result.Failure(PlayerErrors.SquadNumberTaken(request.SquadNumber));
        }

        _dbContext.Players.Add(player);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created 
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotCreated(request.Name, request.DateOfBirth));
    }
}
