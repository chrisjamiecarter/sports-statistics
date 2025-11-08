using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Delete;

internal sealed class DeleteCompetitionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions.Where(competition => competition.Id == request.CompetitionId)
                                                       .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(request.CompetitionId));
        }

        _dbContext.Competitions.Remove(competition);

        var deleted = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return deleted
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotDeleted(competition.Id));
    }
}
