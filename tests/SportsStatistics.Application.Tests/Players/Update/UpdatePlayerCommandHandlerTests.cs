using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandHandlerTests
{
    private static readonly Player BasePlayer = Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper.Name);
    private static readonly UpdatePlayerCommand BaseCommand = new(BasePlayer.Id.Value, "Jack Black", 2, "American", new DateOnly(1992, 2, 2), Position.Defender.Name);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdatePlayerCommandHandler _handler;

    public UpdatePlayerCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdatePlayerCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();
        var players = new[] { BasePlayer };
        var dbSetMock = SetupPlayersDbSet(players, BasePlayer);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        dbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotFound(BasePlayer.Id));
        var players = new List<Player>();
        var dbSetMock = SetupPlayersDbSet(players, null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        dbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSquadNumberIsTakenByCurrentPlayer()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();
        var players = new[] { BasePlayer };
        var dbSetMock = SetupPlayersDbSet(players, BasePlayer);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        dbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSquadNumberIsTakenByAnotherPlayer()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.SquadNumberTaken(command.SquadNumber));
        var existingPlayer = Player.Create("Existing Player", command.SquadNumber, "Nationality", command.DateOfBirth, command.PositionName);
        var players = new[] { BasePlayer, existingPlayer };
        var dbSetMock = SetupPlayersDbSet(players, BasePlayer);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        dbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotUpdated(BasePlayer.Id));
        var players = new[] { BasePlayer };
        var dbSetMock = SetupPlayersDbSet(players, BasePlayer);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        dbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private Mock<DbSet<Player>> SetupPlayersDbSet(IEnumerable<Player> players, Player? findResult = null)
    {
        var mockDbSet = players.ToList().BuildMockDbSet();

        mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(findResult);

        _dbContextMock.Setup(m => m.Players).Returns(mockDbSet.Object);

        return mockDbSet;
    }
}
