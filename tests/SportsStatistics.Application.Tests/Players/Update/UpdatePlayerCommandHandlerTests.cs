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
        PlayerFixtures.Goalkeeper,
        PlayerFixtures.Defender,
        PlayerFixtures.Midfielder,
        PlayerFixtures.Attacker
    ];

    private static readonly UpdatePlayerCommand BaseCommand = new(PlayerFixtures.Goalkeeper.Id,
                                                                  $"{PlayerFixtures.Goalkeeper.Name} Updated",
                                                                  11,
                                                                  $"{PlayerFixtures.Goalkeeper.Nationality} Updated",
                                                                  PlayerFixtures.Goalkeeper.DateOfBirth.Value.AddDays(-1),
                                                                  Position.Midfielder.Value);

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
        var command = BaseCommand with { SquadNumber = PlayerFixtures.Goalkeeper.SquadNumber };
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
        var command = BaseCommand with { SquadNumber = PlayerFixtures.Defender.SquadNumber };
        var expected = Result.Failure(PlayerErrors.SquadNumberTaken(command.SquadNumber));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
