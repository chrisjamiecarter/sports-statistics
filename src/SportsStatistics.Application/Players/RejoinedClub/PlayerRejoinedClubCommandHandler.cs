using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.RejoinedClub;

internal sealed class PlayerRejoinedClubCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<PlayerRejoinedClubCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(PlayerRejoinedClubCommand request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Players
            .Where(player => player.Id == request.PlayerId)
            .SingleOrDefaultAsync(cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        if (!player.LeftClub)
        {
            return Result.Failure(PlayerErrors.AlreadyAtClub);
        }

        player.RejoinClub();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
