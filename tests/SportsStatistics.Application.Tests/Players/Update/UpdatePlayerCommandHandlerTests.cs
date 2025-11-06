using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Update;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Update;

public class UpdatePlayerCommandHandlerTests
{
    private static readonly List<Player> BasePlayers =
    [
        Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper.Name),
        Player.Create("Jack Black", 2, "American", new DateOnly(1992, 2, 2), Position.Defender.Name),
    ];

    private static readonly UpdatePlayerCommand BaseCommand = new(BasePlayers[0].Id,
                                                                  $"{BasePlayers[0].Name} Updated",
                                                                  11,
                                                                  $"{BasePlayers[0].Nationality} Updated",
                                                                  BasePlayers[0].DateOfBirth.AddDays(-1),
                                                                  Position.Midfielder.Name);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdatePlayerCommandHandler _handler;

    public UpdatePlayerCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Players)
                      .Returns(BasePlayers.BuildMockDbSet().Object);

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

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { PlayerId = Guid.CreateVersion7() };
        var expected = Result.Failure(PlayerErrors.NotFound(command.PlayerId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSquadNumberIsTakenByCurrentPlayer()
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = BasePlayers[0].SquadNumber };
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSquadNumberIsTakenByAnotherPlayer()
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = BasePlayers[1].SquadNumber };
        var expected = Result.Failure(PlayerErrors.SquadNumberTaken(command.SquadNumber));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(PlayerErrors.NotUpdated(command.PlayerId));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
