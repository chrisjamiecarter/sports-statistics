using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.GetById;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.GetById;

public class GetSeasonByIdQueryHandlerTests
{
    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(new DateOnly(1990, 07, 01), new DateOnly(1991, 06, 30)),
        Season.Create(new DateOnly(1991, 07, 01), new DateOnly(1992, 06, 30)),
    ];

    private static readonly GetSeasonByIdQuery BaseCommand = new(BaseSeasons[0].Id);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetSeasonByIdQueryHandler _handler;

    public GetSeasonByIdQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _handler = new GetSeasonByIdQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success(BaseSeasons[0].ToResponse());

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNotFound()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = Guid.CreateVersion7() };
        var expected = Result.Failure<SeasonResponse>(SeasonErrors.NotFound(command.SeasonId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeEquivalentTo(expected.IsSuccess);
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }
}
