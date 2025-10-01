using SportsStatistics.Application.Players;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandHandlerTests
{
    private static readonly Player BasePlayer = Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper);
    private static readonly UpdatePlayerCommand BaseCommand = new(BasePlayer.Id.Value, "Jack Black", 2, "American", new DateOnly(1992, 2, 2), Position.Defender.Name);

    private readonly Mock<IPlayerRepository> _repositoryMock;
    private readonly UpdatePlayerCommandHandler _handler;

    public UpdatePlayerCommandHandlerTests()
    {
        _repositoryMock = new Mock<IPlayerRepository>();
        _handler = new UpdatePlayerCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BasePlayer);

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotFound(BasePlayer.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Player?)null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerPositionIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { Position = "Manager" };
        var expected = Result.Failure(PlayerErrors.InvalidPosition(command.Position));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BasePlayer);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotUpdated(BasePlayer.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BasePlayer);

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
