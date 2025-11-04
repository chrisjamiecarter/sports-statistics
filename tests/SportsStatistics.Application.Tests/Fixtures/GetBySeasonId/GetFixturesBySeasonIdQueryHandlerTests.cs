using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetBySeasonId;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetBySeasonId;

public class GetFixturesBySeasonIdQueryHandlerTests
{
    private static readonly List<Competition> BaseCompetitions =
    [
        Competition.Create(EntityId.Create(), "Test Competition", CompetitionType.League.Name),
    ];

    private static readonly List<Fixture> BaseFixtures =
    [
            Fixture.Create(BaseCompetitions[0].Id, "Test Opponent", DateTime.UtcNow.AddDays(7), FixtureLocation.Home.Name),
            Fixture.Create(BaseCompetitions[0].Id, "Test Opponent", DateTime.UtcNow.AddDays(14), FixtureLocation.Away.Name),
    ];

    private static readonly GetFixturesBySeasonIdQuery BaseCommand = new(BaseCompetitions[0].SeasonId);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetFixturesBySeasonIdQueryHandler _handler;

    public GetFixturesBySeasonIdQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _handler = new GetFixturesBySeasonIdQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var competitions = BaseCompetitions;
        var fixtures = BaseFixtures;
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions[0])).ToList());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

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
        var competitions = BaseCompetitions;
        var fixtures = BaseFixtures.Take(1).ToList();
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions[0])).ToList());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

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
        var competitions = BaseCompetitions;
        var fixtures = BaseFixtures.Take(0).ToList();
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions[0])).ToList());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
