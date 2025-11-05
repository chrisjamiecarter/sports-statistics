using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Delete;

internal sealed class DeletePlayerCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeletePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Players.Where(player => player.Id == request.PlayerId)
                                             .SingleOrDefaultAsync(cancellationToken);
        
        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        _dbContext.Players.Remove(player);

        var deleted = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return deleted 
            ? Result.Success()
            : Result.Failure(PlayerErrors.NotDeleted(player.Id));
    }
}
