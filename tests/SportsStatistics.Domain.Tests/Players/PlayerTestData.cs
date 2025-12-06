using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players;

public static class PlayerTestData
{
    public static readonly Name Name = Name.Create("Name").Value;

    public static readonly SquadNumber SquadNumber = SquadNumber.Create(1).Value;

    public static readonly Nationality Nationality = Nationality.Create("Nationality").Value;

    public static readonly DateOfBirth DateOfBirth = DateOfBirth.Create(new(2000, 1, 1)).Value;

    public static readonly Position Position = Position.Goalkeeper;

    public static Player ValidPlayer => Player.Create(Name, SquadNumber, Nationality, DateOfBirth, Position);
}
