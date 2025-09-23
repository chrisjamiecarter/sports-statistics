using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandHandler(ICompetitionRepository repository) : ICommandHandler<UpdateCompetitionCommand>
{
    private readonly ICompetitionRepository _repository = repository;

    public async Task<Result> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var idResult = EntityId.Create(request.Id);
        if (idResult.IsFailure)
        {
            return idResult;
        }

        var id = idResult.Value;

        var competition = await _repository.GetByIdAsync(id, cancellationToken);
        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(id));
        }

        competition.Update(request.Name, request.CompetitionType);

        var updated = await _repository.UpdateAsync(competition, cancellationToken);

        return updated
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotUpdated(id));
    }
}
