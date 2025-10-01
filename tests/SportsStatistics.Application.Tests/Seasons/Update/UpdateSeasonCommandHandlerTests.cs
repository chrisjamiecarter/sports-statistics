using SportsStatistics.Application.Seasons;
using SportsStatistics.Application.Seasons.Update;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Update;

public class UpdateSeasonCommandHandlerTests
{
    private static readonly Season BaseSeason = Season.Create(new DateOnly(2025, 7, 1),
                                                              new DateOnly(2026, 6, 30));
    private static readonly UpdateSeasonCommand BaseCommand = new(BaseSeason.Id.Value, new DateOnly(2025, 8, 2), new DateOnly(2026, 8, 1));

    private readonly Mock<ISeasonRepository> _repositoryMock;
    private readonly UpdateSeasonCommandHandler _handler;

    public UpdateSeasonCommandHandlerTests()
    {
        _repositoryMock = new Mock<ISeasonRepository>();
        _handler = new UpdateSeasonCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSeasonIsUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseSeason);

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(SeasonErrors.NotFound(BaseSeason.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Season?)null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(SeasonErrors.NotUpdated(BaseSeason.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseSeason);

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Season>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
