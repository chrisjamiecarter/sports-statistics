using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players;

public class PlayerTests
{
    [Theory]
    [ClassData(typeof(ValidPlayerTestCase))]
    public void Create_ShouldCreatePlayer_WhenParametersAreValid(Name name, SquadNumber squadNumber, Nationality nationality, DateOfBirth dateOfBirth, Position position, int age)
    {
        // Arrange.
        // Act.
        var player = Player.Create(name,
                                   squadNumber,
                                   nationality,
                                   dateOfBirth,
                                   position);

        // Assert.
        player.ShouldNotBeNull();
        player.Id.ShouldNotBe(default);
        player.Id.Version.ShouldBe(7);
        player.Name.ShouldBe(name);
        player.SquadNumber.ShouldBe(squadNumber);
        player.Nationality.ShouldBe(nationality);
        player.DateOfBirth.ShouldBe(dateOfBirth);
        player.Position.ShouldBe(position);
        player.Age.ShouldBe(age);
        player.Deleted.ShouldBeFalse();
        player.DeletedOnUtc.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(ChangePlayerNameTestCase))]
    public void ChangeName_ShouldChangeName_WhenNameIsDifferent(Player player, Name name)
    {
        // Arrange.
        // Act.
        var result = player.ChangeName(name);

        // Assert.
        result.ShouldBeTrue();
        player.Name.ShouldBe(name);
    }

    [Fact]
    public void ChangeName_ShouldNotChangeName_WhenNameIsIdentical()
    {
        // Arrange.
        var player = PlayerTestData.ValidPlayer;

        // Act.
        var result = player.ChangeName(player.Name);

        // Assert.
        result.ShouldBeFalse();
    }
}
