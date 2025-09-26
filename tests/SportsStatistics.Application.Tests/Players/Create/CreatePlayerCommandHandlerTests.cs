using SportsStatistics.Application.Players;
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

    private readonly Mock<IPlayerRepository> _repositoryMock;
    private readonly CreatePlayerCommandHandler _handler;

    public CreatePlayerCommandHandlerTests()
    {
        _repositoryMock = new Mock<IPlayerRepository>();
        _handler = new CreatePlayerCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPlayerIsNotCreated()
    {
        // Arrange.
        var command = BaseCommand;
        Result? expected = null; // Set in the callback below.

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()))
                       .Callback<Player, CancellationToken>((c, _) =>
                       {
                           expected = Result.Failure(PlayerErrors.NotCreated(c.Id));
                       })
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Player>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
