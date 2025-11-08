using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions.Where(competition => competition.Id == request.CompetitionId)
                                                       .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(request.CompetitionId));
        }

        competition.Update(request.Name, request.CompetitionTypeName);

        var updated = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return updated
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotUpdated(competition.Id));
    }
}
