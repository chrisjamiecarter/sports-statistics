using SportsStatistics.Application.Seasons;
using SportsStatistics.Application.Seasons.GetAll;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.GetAll;

public class GetSeasonsQueryHandlerTests
{
    private static readonly GetSeasonsQuery BaseCommand = new();

    private readonly Mock<ISeasonRepository> _repositoryMock;
    private readonly GetSeasonsQueryHandler _handler;

    public GetSeasonsQueryHandlerTests()
    {
        _repositoryMock = new Mock<ISeasonRepository>();
        _handler = new GetSeasonsQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var seasons = new List<Season>
        {
            Season.Create(new DateOnly(1990, 07, 01), new DateOnly(1991, 06, 30)),
            Season.Create(new DateOnly(1991, 07, 01), new DateOnly(1992, 06, 30)),
        };
        var expected = Result.Success(seasons.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(seasons);

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
        var seasons = new List<Season>
        {
            Season.Create(new DateOnly(1990, 07, 01), new DateOnly(1991, 06, 30)),
        };
        var expected = Result.Success(seasons.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(seasons);

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
        var seasons = new List<Season>();
        var expected = Result.Success(seasons.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(seasons);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
