using SportsStatistics.Application.Seasons;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Create;

public class CreateSeasonCommandHandlerTests
{
    private static readonly CreateSeasonCommand BaseCommand = new(new DateOnly(2025, 7, 1),
                                                                  new DateOnly(2026, 6, 30));

    private readonly Mock<ISeasonRepository> _repositoryMock;
    private readonly CreateSeasonCommandHandler _handler;

    public CreateSeasonCommandHandlerTests()
    {
        _repositoryMock = new Mock<ISeasonRepository>();
        _handler = new CreateSeasonCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSeasonIsCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotCreated()
    {
        // Arrange.
        var command = BaseCommand;
        Result? expected = null; // Set in the callback below.

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()))
                       .Callback<Season, CancellationToken>((c, _) =>
                       {
                           expected = Result.Failure(SeasonErrors.NotCreated(c.Id));
                       })
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
