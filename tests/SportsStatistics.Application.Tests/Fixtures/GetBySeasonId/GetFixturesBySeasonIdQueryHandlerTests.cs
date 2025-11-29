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
        FixtureFixtures.CompetitionLeague2024_2025,
        FixtureFixtures.CompetitionCup2024_2025,
    ];

    private static readonly List<Fixture> BaseFixtures =
    [
        FixtureFixtures.FixtureGW1League2024_2925,
        FixtureFixtures.FixtureR1Cup2024_2925,
    ];

    private static readonly GetFixturesBySeasonIdQuery BaseCommand = new(FixtureFixtures.Season2024_2025.Id);

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
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions.First(competition => fixture.CompetitionId == competition.Id))).ToList());

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
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions.First(competition => fixture.CompetitionId == competition.Id))).ToList());

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
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions.First(competition => fixture.CompetitionId == competition.Id))).ToList());

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
