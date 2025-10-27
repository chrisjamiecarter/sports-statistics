using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Update;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Update;

public class UpdateSeasonCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(new DateOnly(2023, 8, 1), new DateOnly(2024, 7, 31)),
        Season.Create(new DateOnly(2024, 8, 1), new DateOnly(2025, 7, 31)),
    ];

    private static readonly UpdateSeasonCommand BaseCommand = new(BaseSeasons[0].Id,
                                                                  BaseSeasons[0].StartDate.AddDays(1),
                                                                  BaseSeasons[0].EndDate.AddDays(-1));

    private readonly Mock<DbSet<Season>> _seasonDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateSeasonCommandHandler _handler;

    public UpdateSeasonCommandHandlerTests()
    {
        _seasonDbSetMock = BaseSeasons.BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasonDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateSeasonCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSeasonIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = Guid.CreateVersion7() };
        var expected = Result.Failure(SeasonErrors.NotFound(command.SeasonId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(SeasonErrors.NotUpdated(command.SeasonId));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);
        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
