namespace SportsStatistics.Web.Players;

public sealed record PlayerDto(Guid Id,
                                 string Name,
                                 string Role,
                                 int SquadNumber,
                                 string Nationality,
                                 int Age)
{
    public static PlayerDto CreateBlank()
        => new(Guid.CreateVersion7(), string.Empty, string.Empty, 0, string.Empty, 0);
}
