using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public static class PlayerTestData
{
    public static readonly Name Name = NameTestData.ValidName;

    public static readonly SquadNumber SquadNumber = SquadNumberTestData.ValidSquadNumber;

    public static readonly Nationality Nationality = NationalityTestData.ValidNationality;

    public static readonly DateOfBirth DateOfBirth = DateOfBirthTestData.ValidDateOfBirth;

    public static readonly Position Position = PositionTestData.ValidPosition;

    public static Player ValidPlayer => Player.Create(Name, SquadNumber, Nationality, DateOfBirth, Position);

    public static Player ValidPlayerWithName(Name name) => Player.Create(name, SquadNumber, Nationality, DateOfBirth, Position);
}
