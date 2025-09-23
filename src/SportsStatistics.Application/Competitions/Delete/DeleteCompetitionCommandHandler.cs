using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Delete;

internal sealed class DeleteCompetitionCommandHandler(ICompetitionRepository repository) : ICommandHandler<DeleteCompetitionCommand>
{
    private readonly ICompetitionRepository _repository = repository;

    public async Task<Result> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
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

        var deleted = await _repository.DeleteAsync(competition, cancellationToken);

        return deleted
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotDeleted(id));
    }
}
