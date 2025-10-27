using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons;
using SportsStatistics.Application.Seasons.GetAll;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.GetAll;

public class GetSeasonsQueryHandlerTests
{
    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(new DateOnly(1990, 07, 01), new DateOnly(1991, 06, 30)),
        Season.Create(new DateOnly(1991, 07, 01), new DateOnly(1992, 06, 30)),
    ];

    private static readonly GetSeasonsQuery BaseCommand = new();

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetSeasonsQueryHandler _handler;

    public GetSeasonsQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        _handler = new GetSeasonsQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var seasons = BaseSeasons;
        var expected = Result.Success(seasons.ToResponse());

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(seasons.BuildMockDbSet().Object);

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
        var seasons = BaseSeasons.Take(1).ToList();
        var expected = Result.Success(seasons.ToResponse());

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(seasons.BuildMockDbSet().Object);

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
        var seasons = BaseSeasons.Take(0).ToList();
        var expected = Result.Success(seasons.ToResponse());

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(seasons.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
