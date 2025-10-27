using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Create;

internal sealed class CreateSeasonCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateSeasonCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = Season.Create(request.StartDate,
                                   request.EndDate);

        _dbContext.Seasons.Add(season);

        var created = await _dbContext.SaveChangesAsync(cancellationToken) > 0;

        return created
            ? Result.Success()
            : Result.Failure(SeasonErrors.NotCreated(request.StartDate, request.EndDate));
    }
}
