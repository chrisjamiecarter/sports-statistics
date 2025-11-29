using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Tests.Players;

public static class PlayerFixtures
{
    public static readonly Player Goalkeeper =
        Player.Create(
            Name.Create("Goalkeeper Name").Value,
            SquadNumber.Create(1).Value,
            Nationality.Create("Goalkeeper Country").Value,
            DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30))).Value,
            Position.Goalkeeper);

    public static readonly Player Defender =
        Player.Create(
            Name.Create("Defender Name").Value,
            SquadNumber.Create(4).Value,
            Nationality.Create("Defender Country").Value,
            DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25))).Value,
            Position.Defender);

    public static readonly Player Midfielder =
        Player.Create(
            Name.Create("Midfielder Name").Value,
            SquadNumber.Create(7).Value,
            Nationality.Create("Midfielder Country").Value,
            DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20))).Value,
            Position.Midfielder);

    public static readonly Player Attacker =
        Player.Create(
            Name.Create("Attacker Name").Value,
            SquadNumber.Create(10).Value,
            Nationality.Create("Attacker Country").Value,
            DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18))).Value,
            Position.Attacker);
}
