using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Update;

public class UpdateFixtureCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(new DateOnly(2025, 8, 1), new DateOnly(2026, 7, 31)),
    ];

    private static readonly List<Competition> BaseCompetitions =
    [
        Competition.Create(BaseSeasons[0].Id, "Test League", CompetitionType.League.Name),
        Competition.Create(BaseSeasons[0].Id, "Test Cup", CompetitionType.Cup.Name),
    ];

    private static readonly List<Fixture> BaseFixtures =
    [
        Fixture.Create(BaseCompetitions[0].Id, "Test Opponent", BaseSeasons[0].StartDate.ToDateTime(TimeOnly.MinValue), FixtureLocation.Home.Name),
    ];

    private static readonly UpdateFixtureCommand BaseCommand = new(BaseFixtures[0].Id,
                                                                   BaseFixtures[0].Opponent,
                                                                   BaseFixtures[0].KickoffTimeUtc.AddDays(1),
                                                                   FixtureLocation.Neutral.Name);

    private readonly Mock<DbSet<Competition>> _competitionDbSetMock;
    private readonly Mock<DbSet<Fixture>> _fixtureDbSetMock;
    private readonly Mock<DbSet<Season>> _seasonDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateFixtureCommandHandler _handler;

    public UpdateFixtureCommandHandlerTests()
    {
        _competitionDbSetMock = BaseCompetitions.BuildMockDbSet();
        _fixtureDbSetMock = BaseFixtures.BuildMockDbSet();
        _seasonDbSetMock = BaseSeasons.BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitionDbSetMock.Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(_fixtureDbSetMock.Object);

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasonDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateFixtureCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { FixtureId = Guid.CreateVersion7() };
        var expected = Result.Failure(FixtureErrors.NotFound(command.FixtureId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(FixtureErrors.NotUpdated(command.FixtureId));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
