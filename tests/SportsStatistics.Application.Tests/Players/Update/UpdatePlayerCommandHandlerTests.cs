using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandHandlerTests
{
    private static readonly Player BasePlayer = Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper.Name);
    private static readonly UpdatePlayerCommand BaseCommand = new(BasePlayer.Id.Value, "Jack Black", 2, "American", new DateOnly(1992, 2, 2), Position.Defender.Name);

    private readonly Mock<DbSet<Player>> _playerDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly Mock<IPlayerService> _playerServiceMock;
    private readonly UpdatePlayerCommandHandler _handler;

    public UpdatePlayerCommandHandlerTests()
    {
        _playerDbSetMock = new Mock<DbSet<Player>>();
        _dbContextMock = new Mock<IApplicationDbContext>();
        _playerServiceMock = new Mock<IPlayerService>();

        _dbContextMock.Setup(m => m.Players)
                      .Returns(_playerDbSetMock.Object);

        _playerDbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(BasePlayer);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _playerServiceMock.Setup(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(true);

        _handler = new UpdatePlayerCommandHandler(_dbContextMock.Object, _playerServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotFound(BasePlayer.Id));

        _playerDbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Player?)null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSquadNumberIsTakenByCurrentPlayer()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();
        var entityId = EntityId.Create(command.Id);

        _playerServiceMock.Setup(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSquadNumberIsTakenByAnotherPlayer()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.SquadNumberNotAvailable(command.SquadNumber));

        _playerServiceMock.Setup(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotUpdated(BasePlayer.Id));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
