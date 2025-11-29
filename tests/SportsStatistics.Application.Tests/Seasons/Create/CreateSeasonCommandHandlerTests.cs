using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Create;

public class CreateSeasonCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons =
    [
        SeasonFixtures.Season2023_2024,
        SeasonFixtures.Season2024_2025
    ];

    private static readonly CreateSeasonCommand BaseCommand = new(SeasonFixtures.Season2024_2025.DateRange.StartDate.AddYears(1),
                                                                  SeasonFixtures.Season2024_2025.DateRange.EndDate.AddYears(1));

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateSeasonCommandHandler _handler;

    public CreateSeasonCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new CreateSeasonCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSeasonIsCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
