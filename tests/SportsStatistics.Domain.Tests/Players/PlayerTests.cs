using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players;

public class PlayerTests
{
    [Theory]
    [ClassData(typeof(CreatePlayerTestCase))]
    public void Create_ShouldCreatePlayer_WhenParametersAreValid(Name name, SquadNumber squadNumber, Nationality nationality, DateOfBirth dateOfBirth, Position position)
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
        player.Deleted.ShouldBeFalse();
        player.DeletedOnUtc.ShouldBeNull();
    }

    [Theory]
    [ClassData(typeof(PlayerCreatedDomainEventTestCase))]
    public void Create_ShouldRaisePlayerCreatedDomainEvent_WhenCreatingUser(Name name, SquadNumber squadNumber, Nationality nationality, DateOfBirth dateOfBirth, Position position)
    {
        // Arrange.
        // Act.
        var player = Player.Create(name,
                                   squadNumber,
                                   nationality,
                                   dateOfBirth,
                                   position);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(new PlayerCreatedDomainEvent(player.Id));
    }

    [Theory]
    [ClassData(typeof(ChangeNameDifferentTestCase))]
    public void ChangeName_ShouldChangeName_WhenNameIsDifferent(Player player, Name name)
    {
        // Arrange.
        // Act.
        var result = player.ChangeName(name);

        // Assert.
        result.ShouldBeTrue();
        player.Name.ShouldBe(name);
    }

    [Theory]
    [ClassData(typeof(PlayerNameChangedDomainEventTestCase))]
    public void ChangeName_ShouldRaisePlayerNameChangedDomainEvent_WhenNameIsChanged(Player player, Name name, PlayerNameChangedDomainEvent expected)
    {
        // Arrange.
        player.ClearDomainEvents();

        // Act.
        player.ChangeName(name);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeNameIdenticalTestCase))]
    public void ChangeName_ShouldNotChangeName_WhenNameIsIdentical(Player player, Name name)
    {
        // Arrange.
        // Act.
        var result = player.ChangeName(name);

        // Assert.
        result.ShouldBeFalse();
        player.Name.ShouldBe(name);
    }

    [Theory]
    [ClassData(typeof(ChangeSquadNumberDifferentTestCase))]
    public void ChangeSquadNumber_ShouldChangeSquadNumber_WhenSquadNumberIsDifferent(Player player, SquadNumber squadNumber)
    {
        // Arrange.
        // Act.
        var result = player.ChangeSquadNumber(squadNumber);

        // Assert.
        result.ShouldBeTrue();
        player.SquadNumber.ShouldBe(squadNumber);
    }

    [Theory]
    [ClassData(typeof(PlayerSquadNumberChangedDomainEventTestCase))]
    public void ChangeSquadNumber_ShouldRaisePlayerSquadNumberChangedDomainEvent_WhenSquadNumberIsChanged(Player player, SquadNumber squadNumber, PlayerSquadNumberChangedDomainEvent expected)
    {
        // Arrange.
        player.ClearDomainEvents();

        // Act.
        player.ChangeSquadNumber(squadNumber);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeSquadNumberIdenticalTestCase))]
    public void ChangeSquadNumber_ShouldNotChangeSquadNumber_WhenSquadNumberIsIdentical(Player player, SquadNumber squadNumber)
    {
        // Arrange.
        // Act.
        var result = player.ChangeSquadNumber(squadNumber);

        // Assert.
        result.ShouldBeFalse();
        player.SquadNumber.ShouldBe(squadNumber);
    }

    [Theory]
    [ClassData(typeof(ChangeNationalityDifferentTestCase))]
    public void ChangeNationality_ShouldChangeNationality_WhenNationalityIsDifferent(Player player, Nationality nationality)
    {
        // Arrange.
        // Act.
        var result = player.ChangeNationality(nationality);

        // Assert.
        result.ShouldBeTrue();
        player.Nationality.ShouldBe(nationality);
    }

    [Theory]
    [ClassData(typeof(PlayerNationalityChangedDomainEventTestCase))]
    public void ChangeNationality_ShouldRaisePlayerNationalityChangedDomainEvent_WhenNationalityIsChanged(Player player, Nationality nationality, PlayerNationalityChangedDomainEvent expected)
    {
        // Arrange.
        player.ClearDomainEvents();

        // Act.
        player.ChangeNationality(nationality);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeNationalityIdenticalTestCase))]
    public void ChangeNationality_ShouldNotChangeNationality_WhenNationalityIsIdentical(Player player, Nationality nationality)
    {
        // Arrange.
        // Act.
        var result = player.ChangeNationality(nationality);

        // Assert.
        result.ShouldBeFalse();
        player.Nationality.ShouldBe(nationality);
    }

    [Theory]
    [ClassData(typeof(ChangeDateOfBirthDifferentTestCase))]
    public void ChangeDateOfBirth_ShouldChangeDateOfBirth_WhenDateOfBirthIsDifferent(Player player, DateOfBirth dateOfBirth)
    {
        // Arrange.
        // Act.
        var result = player.ChangeDateOfBirth(dateOfBirth);

        // Assert.
        result.ShouldBeTrue();
        player.DateOfBirth.ShouldBe(dateOfBirth);
    }

    [Theory]
    [ClassData(typeof(PlayerDateOfBirthChangedDomainEventTestCase))]
    public void ChangeDateOfBirth_ShouldRaisePlayerDateOfBirthChangedDomainEvent_WhenDateOfBirthIsChanged(Player player, DateOfBirth dateOfBirth, PlayerDateOfBirthChangedDomainEvent expected)
    {
        // Arrange.
        player.ClearDomainEvents();

        // Act.
        player.ChangeDateOfBirth(dateOfBirth);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangeDateOfBirthIdenticalTestCase))]
    public void ChangeDateOfBirth_ShouldNotChangeDateOfBirth_WhenDateOfBirthIsIdentical(Player player, DateOfBirth dateOfBirth)
    {
        // Arrange.
        // Act.
        var result = player.ChangeDateOfBirth(dateOfBirth);

        // Assert.
        result.ShouldBeFalse();
        player.DateOfBirth.ShouldBe(dateOfBirth);
    }

    [Theory]
    [ClassData(typeof(ChangePositionDifferentTestCase))]
    public void ChangePosition_ShouldChangePosition_WhenPositionIsDifferent(Player player, Position position)
    {
        // Arrange.
        // Act.
        var result = player.ChangePosition(position);

        // Assert.
        result.ShouldBeTrue();
        player.Position.ShouldBe(position);
    }

    [Theory]
    [ClassData(typeof(PlayerPositionChangedDomainEventTestCase))]
    public void ChangePosition_ShouldRaisePlayerPositionChangedDomainEvent_WhenPositionIsChanged(Player player, Position position, PlayerPositionChangedDomainEvent expected)
    {
        // Arrange.
        player.ClearDomainEvents();

        // Act.
        player.ChangePosition(position);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(ChangePositionIdenticalTestCase))]
    public void ChangePosition_ShouldNotChangePosition_WhenPositionIsIdentical(Player player, Position position)
    {
        // Arrange.
        // Act.
        var result = player.ChangePosition(position);

        // Assert.
        result.ShouldBeFalse();
        player.Position.ShouldBe(position);
    }

    [Theory]
    [ClassData(typeof(DeletePlayerTestCase))]
    public void Delete_ShouldDeletePlayer_WhenPlayerIsNotAlreadyDeleted(Player player, DateTime utcNow)
    {
        // Arrange.
        // Act.
        player.Delete(utcNow);

        // Assert.
        player.Deleted.ShouldBeTrue();
        player.DeletedOnUtc.ShouldBe(utcNow);
    }

    [Theory]
    [ClassData(typeof(PlayerDeletedDomainEventTestCase))]
    public void Delete_ShouldRaisePlayerDeletedDomainEvent_WhenPlayerIsDeleted(Player player, DateTime utcNow, PlayerDeletedDomainEvent expected)
    {
        // Arrange.
        player.ClearDomainEvents();

        // Act.
        player.Delete(utcNow);

        // Assert.
        player.DomainEvents.ShouldHaveSingleItem()
                           .ShouldBeEquivalentTo(expected);
    }
}
