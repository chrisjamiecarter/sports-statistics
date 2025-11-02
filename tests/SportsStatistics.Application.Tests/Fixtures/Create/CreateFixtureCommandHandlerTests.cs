using Microsoft.EntityFrameworkCore;
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
        Season.Create(new DateOnly(2025, 8, 1), new DateOnly(2026, 7, 31)),
    ];

    private static readonly List<Competition> BaseCompetitions =
    [
        Competition.Create(BaseSeasons[0].Id, "Test League", CompetitionType.League.Name),
        Competition.Create(BaseSeasons[0].Id, "Test Cup", CompetitionType.Cup.Name),
    ];

    private static readonly CreateFixtureCommand BaseCommand = new(BaseCompetitions[0].Id,
                                                                   "Test Opponent",
                                                                   BaseSeasons[0].StartDate.ToDateTime(TimeOnly.MinValue),
                                                                   FixtureLocation.Home.Name);

    private readonly Mock<DbSet<Competition>> _competitionDbSetMock;
    private readonly Mock<DbSet<Fixture>> _fixtureDbSetMock;
    private readonly Mock<DbSet<Season>> _seasonDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateFixtureCommandHandler _handler;

    public CreateFixtureCommandHandlerTests()
    {
        _competitionDbSetMock = BaseCompetitions.BuildMockDbSet();
        _fixtureDbSetMock = new List<Fixture>().BuildMockDbSet();
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
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotCreatedKickoffTimeOutsideSeason()
    {
        // Arrange.
        var command = BaseCommand with { KickoffTimeUtc = BaseSeasons[0].StartDate.AddDays(-1).ToDateTime(TimeOnly.MinValue) };
        var expected = Result.Failure(FixtureErrors.KickoffTimeOutsideSeason(command.KickoffTimeUtc, BaseSeasons[0].StartDate, BaseSeasons[0].EndDate));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(FixtureErrors.NotCreated(command.Opponent, command.KickoffTimeUtc, command.FixtureLocationName));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
