using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.GetAll;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.GetAll;

public class GetAllPlayersQueryHandlerTests
{
    private static readonly GetAllPlayersQuery BaseCommand = new();

    private readonly Mock<DbSet<Player>> _playerDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetAllPlayersQueryHandler _handler;

    public GetAllPlayersQueryHandlerTests()
    {
        _playerDbSetMock = new Mock<DbSet<Player>>();
        _dbContextMock = new Mock<IApplicationDbContext>();

        _handler = new GetAllPlayersQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var players = new List<Player>
        {
            Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper.Name),
            Player.Create("Jack Black", 2, "American", new DateOnly(1992, 2, 2), Position.Defender.Name),
        };
        var expected = Result.Success(players.ToResponse());

        _dbContextMock.Setup(m => m.Players)
                      .Returns(players.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenOne()
    {
        // Arrange.
        var command = BaseCommand;
        var players = new List<Player>
        {
            Player.Create("John Smith", 1, "British", new DateOnly(1991, 1, 1), Position.Goalkeeper.Name),
        };
        var expected = Result.Success(players.ToResponse());

        _dbContextMock.Setup(m => m.Players)
                      .Returns(players.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNone()
    {
        // Arrange.
        var command = BaseCommand;
        var players = new List<Player>();
        var expected = Result.Success(players.ToResponse());

        _dbContextMock.Setup(m => m.Players)
                      .Returns(players.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
