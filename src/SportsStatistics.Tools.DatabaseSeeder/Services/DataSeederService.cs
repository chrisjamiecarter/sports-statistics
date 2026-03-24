namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IDataSeederService
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}

internal sealed class DataSeederService
    : IDataSeederService
{
    public Task SeedAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
