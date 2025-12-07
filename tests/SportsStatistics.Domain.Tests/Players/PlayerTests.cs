using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class PlayerTests
{
    [Fact]
    public void Create_ShouldCreatePlayer_WhenParametersAreValid()
    {
        // Arrange.
        // Act.
        var player = Player.Create(PlayerTestData.Name,
                                   PlayerTestData.SquadNumber,
                                   PlayerTestData.Nationality,
                                   PlayerTestData.DateOfBirth,
                                   PlayerTestData.Position);

        // Assert.
        player.ShouldNotBeNull();
        player.Id.ShouldNotBe(default);
        player.Id.Version.ShouldBe(7);
        player.Name.ShouldBe(PlayerTestData.Name);
        player.SquadNumber.ShouldBe(PlayerTestData.SquadNumber);
        player.Nationality.ShouldBe(PlayerTestData.Nationality);
        player.DateOfBirth.ShouldBe(PlayerTestData.DateOfBirth);
        player.Position.ShouldBe(PlayerTestData.Position);
        player.Age.ShouldBe(PlayerTestData.DateOfBirth.Value.CalculateAge());
        player.Deleted.ShouldBeFalse();
        player.DeletedOnUtc.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(ChangeNameData))]
    public void ChangeName_ShouldChangeName_WhenNameIsDifferent(Name oldName, Name newName)
    {
        // Arrange.
        var player = PlayerTestData.ValidPlayerWithName(oldName);

        // Act.
        var result = player.ChangeName(newName);

        // Assert.
        result.ShouldBeTrue();
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
