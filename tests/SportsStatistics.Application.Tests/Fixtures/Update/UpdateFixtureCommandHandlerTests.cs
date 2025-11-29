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
        FixtureFixtures.FixtureR1Cup2024_2925,
    ];

    private static readonly UpdateFixtureCommand BaseCommand = new(FixtureFixtures.FixtureGW1League2024_2925.Id,
                                                                   $"{FixtureFixtures.FixtureGW1League2024_2925.Opponent.Value} Updated",
                                                                   FixtureFixtures.FixtureGW1League2024_2925.KickoffTimeUtc.Value.AddDays(1),
                                                                   Location.Neutral.Value);

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
        var command = BaseCommand with { KickoffTimeUtc = FixtureFixtures.FixtureGW1League2024_2925.KickoffTimeUtc };
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
        var command = BaseCommand with { KickoffTimeUtc = FixtureFixtures.FixtureR1Cup2024_2925.KickoffTimeUtc };
        var expected = Result.Failure(FixtureErrors.AlreadyScheduledOnDate(DateOnly.FromDateTime(command.KickoffTimeUtc)));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
