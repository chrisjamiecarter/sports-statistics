using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Delete;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Delete;

public class DeletePlayerCommandHandlerTests
{
    private static readonly Player BasePlayer = Player.Create("Test Name",
                                                              1,
                                                              "Test Nationality",
                                                              DateOnly.FromDateTime(DateTime.Now.AddYears(-15)),
                                                              Position.Goalkeeper.Name);
    private static readonly DeletePlayerCommand BaseCommand = new(BasePlayer.Id.Value);

    private readonly Mock<DbSet<Player>> _playerDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly DeletePlayerCommandHandler _handler;

    public DeletePlayerCommandHandlerTests()
    {
        _playerDbSetMock = new Mock<DbSet<Player>>();
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Players)
                      .Returns(_playerDbSetMock.Object);

        _playerDbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(BasePlayer);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new DeletePlayerCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerDbSetMock.Verify(m => m.Remove(It.IsAny<Player>()), Times.Once);
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
        _playerDbSetMock.Verify(m => m.Remove(It.IsAny<Player>()), Times.Never);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotDeleted(BasePlayer.Id));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerDbSetMock.Verify(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerDbSetMock.Verify(m => m.Remove(It.IsAny<Player>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
