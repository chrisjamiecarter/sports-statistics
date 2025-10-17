using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateFixtureCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdateFixtureCommand request, CancellationToken cancellationToken)
    {
        var entityId = EntityId.Create(request.Id);

        var fixture = await _dbContext.Fixtures.AsNoTracking()
                                               .SingleOrDefaultAsync(f => f.Id == entityId, cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(entityId));
        }

        fixture.Update(request.Opponent, request.KickoffTimeUtc, request.FixtureLocationName);

        var updated = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return updated
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotUpdated(fixture.Id));
    }
}
