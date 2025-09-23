using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Create;

internal sealed class CreateCompetitionCommandHandler(ICompetitionRepository repository) : ICommandHandler<CreateCompetitionCommand>
{
    private readonly ICompetitionRepository _repository = repository;

    public async Task<Result> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competitionType = CompetitionType.FromName(request.CompetitionType);

        var competition = Competition.Create(request.Name, competitionType);

        var created = await _repository.CreateAsync(competition, cancellationToken);

        return created
            ? Result.Success()
            : Result.Failure(CompetitionErrors.NotCreated(competition.Id));
    }
}
