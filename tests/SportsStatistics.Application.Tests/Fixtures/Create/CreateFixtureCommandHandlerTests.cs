using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Create;

public class CreateFixtureCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons =
    [
        FixtureFixtures.Season2023_2024,
        FixtureFixtures.Season2024_2025,
    ];

    private static readonly List<Competition> BaseCompetitions =
    [
        FixtureFixtures.CompetitionLeague2024_2025,
        FixtureFixtures.CompetitionCup2024_2025,
    ];

    private static readonly List<Fixture> BaseFixtures =
    [
        FixtureFixtures.FixtureGW1League2024_2925,
    ];

    private static readonly CreateFixtureCommand BaseCommand = new(FixtureFixtures.CompetitionLeague2024_2025.Id,
                                                                   "Test Opponent",
                                                                   FixtureFixtures.FixtureGW1League2024_2925.KickoffTimeUtc.Value.AddDays(7),
                                                                   Location.Away.Value);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateFixtureCommandHandler _handler;

    public CreateFixtureCommandHandlerTests()
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

        _handler = new CreateFixtureCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsCreated()
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
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = Guid.CreateVersion7() };
        var expected = Result.Failure(CompetitionErrors.NotFound(command.CompetitionId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenKickoffTimeOutsideSeason()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = FixtureFixtures.Season2024_2025.DateRange.StartDate.AddDays(-1).ToDateTime(TimeOnly.MinValue) };
        var expected = Result.Failure(FixtureErrors.KickoffTimeOutsideSeason(command.KickoffTimeUtc, FixtureFixtures.Season2024_2025.DateRange.StartDate, FixtureFixtures.Season2024_2025.DateRange.EndDate));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAnotherFixtureAlreadyScheduled()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = FixtureFixtures.FixtureGW1League2024_2925.KickoffTimeUtc };
        var expected = Result.Failure(FixtureErrors.AlreadyScheduledOnDate(DateOnly.FromDateTime(command.KickoffTimeUtc)));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
