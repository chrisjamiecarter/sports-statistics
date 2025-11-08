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
        var fixture = await _dbContext.Fixtures.Where(fixture => fixture.Id == request.FixtureId)
                                               .SingleOrDefaultAsync(cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        _dbContext.Fixtures.Remove(fixture);

        var deleted = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return deleted
            ? Result.Success()
            : Result.Failure(FixtureErrors.NotDeleted(fixture.Id));
    }
}
