namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IRoleSeederService
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}

internal sealed class RoleSeederService
    : IRoleSeederService
{
    public Task SeedAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

internal interface IUserSeederService
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}

internal sealed class UserSeederService
    : IUserSeederService
{
    public Task SeedAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

internal interface IClubSeederService
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}

internal sealed class ClubSeederService
    : IClubSeederService
{
    public Task SeedAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

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
