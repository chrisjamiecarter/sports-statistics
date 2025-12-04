using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Delete;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Delete;

public class DeletePlayerCommandHandlerTests
{
    private static readonly List<Player> BasePlayers = PlayerBuilder.GetDefaults();

    private static readonly DeletePlayerCommand BaseCommand = new(BasePlayers.First().Id);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly DeletePlayerCommandHandler _handler;

    public DeletePlayerCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Players)
                      .Returns(BasePlayers.BuildMockDbSet().Object);

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
}
