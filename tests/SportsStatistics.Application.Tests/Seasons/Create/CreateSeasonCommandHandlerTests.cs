using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Create;

public class CreateSeasonCommandHandlerTests
{
    private static readonly CreateSeasonCommand BaseCommand = new(new DateOnly(2025, 8, 1),
                                                                  new DateOnly(2026, 7, 31));

    private readonly Mock<DbSet<Season>> _seasonDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateSeasonCommandHandler _handler;

    public CreateSeasonCommandHandlerTests()
    {
        _seasonDbSetMock = new List<Season>().BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasonDbSetMock.Object);

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

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(SeasonErrors.NotCreated(command.StartDate, command.EndDate));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
