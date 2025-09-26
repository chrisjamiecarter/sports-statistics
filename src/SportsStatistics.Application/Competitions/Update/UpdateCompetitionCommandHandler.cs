using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandHandler(ICompetitionRepository repository) : ICommandHandler<UpdateCompetitionCommand>
{
    private readonly ICompetitionRepository _repository = repository;

    public async Task<Result> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var id = EntityId.Create(request.Id);

        var competition = await _repository.GetByIdAsync(id, cancellationToken);
        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(id));
        }

        var competitionType = CompetitionType.FromName(request.CompetitionType);

        competition.Update(request.Name, competitionType);

        var updated = await _repository.UpdateAsync(competition, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotUpdated(id));
    }
}
