using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Delete;

internal sealed class DeleteFixtureCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteFixtureCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(DeleteFixtureCommand request, CancellationToken cancellationToken)
    {
        var entityId = EntityId.Create(request.Id);

        var fixture = await _dbContext.Fixtures.AsNoTracking()
                                               .SingleOrDefaultAsync(f => f.Id == entityId, cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(entityId));
        }

        _dbContext.Fixtures.Remove(fixture);

        var deleted = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return deleted
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotDeleted(fixture.Id));
    }
}
