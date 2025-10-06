using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Create;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Create;

public class CreatePlayerCommandHandlerTests
{
    private static readonly CreatePlayerCommand BaseCommand = new("Test Name",
                                                                  1,
                                                                  "Test Nationality",
                                                                  DateOnly.FromDateTime(DateTime.Today).AddYears(-15),
                                                                  Position.Goalkeeper.Name);

    private readonly Mock<DbSet<Player>> _playerDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly Mock<IPlayerService> _playerServiceMock;
    private readonly CreatePlayerCommandHandler _handler;

    public CreatePlayerCommandHandlerTests()
    {
        _playerDbSetMock = new Mock<DbSet<Player>>();
        _dbContextMock = new Mock<IApplicationDbContext>();
        _playerServiceMock = new Mock<IPlayerService>();

        _dbContextMock.Setup(m => m.Players)
                      .Returns(_playerDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _playerServiceMock.Setup(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

        _handler = new CreatePlayerCommandHandler(_dbContextMock.Object, _playerServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerDbSetMock.Verify(m => m.Add(It.IsAny<Player>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSquadNumberIsUnavailable()
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
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerDbSetMock.Verify(m => m.Add(It.IsAny<Player>()), Times.Never);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotCreated(command.Name, command.DateOfBirth));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _playerServiceMock.Verify(m => m.IsSquadNumberAvailableAsync(It.IsAny<EntityId>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        _playerDbSetMock.Verify(m => m.Add(It.IsAny<Player>()), Times.Once);
        _dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
