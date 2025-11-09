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
    private static readonly DateOnly BaseSeasonStartDate = new(2025, 8, 1);
    private static readonly DateOnly BaseSeasonEndDate = new(2026, 7, 31);


    private static readonly List<Season> BaseSeasons =
    [
        Season.Create(BaseSeasonStartDate, BaseSeasonEndDate),
    ];

    private static readonly List<Competition> BaseCompetitions =
    [
        Competition.Create(BaseSeasons[0].Id, "Test League", CompetitionType.League.Name),
        Competition.Create(BaseSeasons[0].Id, "Test Cup", CompetitionType.Cup.Name),
    ];

    private static readonly List<Fixture> BaseFixtures =
    [
        Fixture.Create(BaseCompetitions[0].Id, "Test Opponent", BaseSeasonStartDate.ToDateTime(TimeOnly.MinValue), FixtureLocation.Home.Name),
        Fixture.Create(BaseCompetitions[0].Id, "Test Opponent", BaseSeasonStartDate.AddDays(7).ToDateTime(TimeOnly.MinValue), FixtureLocation.Away.Name),
    ];

    private static readonly UpdateFixtureCommand BaseCommand = new(BaseFixtures[0].Id,
                                                                   $"{BaseFixtures[0].Opponent} Updated",
                                                                   BaseFixtures[0].KickoffTimeUtc.AddDays(1),
                                                                   FixtureLocation.Neutral.Name);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateFixtureCommandHandler _handler;

    public UpdateFixtureCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(BaseCompetitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(BaseFixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

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
    public async Task Handle_ShouldReturnSuccess_WhenCurrentFixtureIsAlreadyScheduled()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = BaseFixtures[0].KickoffTimeUtc };
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAnotherFixtureAlreadyScheduled()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = BaseFixtures[1].KickoffTimeUtc };
        var expected = Result.Failure(FixtureErrors.AlreadyScheduledOnDate(DateOnly.FromDateTime(command.KickoffTimeUtc)));

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
