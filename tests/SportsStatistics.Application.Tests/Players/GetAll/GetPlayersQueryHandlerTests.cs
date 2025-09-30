using SportsStatistics.Application.Players;
using SportsStatistics.Application.Players.GetAll;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.GetAll;

public class GetPlayersQueryHandlerTests
{
    private static readonly GetPlayersQuery BaseCommand = new();

    private readonly Mock<IPlayerRepository> _repositoryMock;
    private readonly GetPlayersQueryHandler _handler;

    public GetPlayersQueryHandlerTests()
    {
        _repositoryMock = new Mock<IPlayerRepository>();
        _handler = new GetPlayersQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var players = new List<Player>
        {
            Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper),
            Player.Create("Jack Black", 2, "American", new DateOnly(1992, 2, 2), Position.Defender),
        };
        var expected = Result.Success(players.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(players);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenOne()
    {
        // Arrange.
        var command = BaseCommand;
        var players = new List<Player>
        {
            Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper),
        };
        var expected = Result.Success(players.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(players);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNone()
    {
        // Arrange.
        var command = BaseCommand;
        var players = new List<Player>();
        var expected = Result.Success(players.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(players);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
