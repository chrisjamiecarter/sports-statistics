using Microsoft.EntityFrameworkCore;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IDataSeederService : ISeederService { }

internal sealed class DataSeederService(
    ApplicationDbContext dbContext,
    IPlayerSeederService playerSeederService,
    ISeasonSeederService seasonSeederService)
    : IDataSeederService
{
    //private static readonly IReadOnlyList<string> OppositionTeams =
    //[
    //    "Riverside Rangers",
    //    "Oakwood United",
    //    "Steel City FC",
    //    "Harborview Athletic",
    //    "Parkland Wanderers",
    //    "Meadowbrook City",
    //    "Ironbridge Town",
    //    "Crestview Albion",
    //    "Bayshore United",
    //    "Valley Rovers"
    //];

    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IPlayerSeederService _playerSeederService = playerSeederService;
    private readonly ISeasonSeederService _seasonSeederService = seasonSeederService;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Players.AnyAsync(cancellationToken) || 
            await _dbContext.Seasons.AnyAsync(cancellationToken))
        {
            return Result.Success();
        }

        var playerResult = await _playerSeederService.SeedAsync(cancellationToken);
        var seasonResult = await _seasonSeederService.SeedAsync(cancellationToken);
        
        return Result.FirstFailureOrSuccess(
            playerResult,
            seasonResult);
    }
}
