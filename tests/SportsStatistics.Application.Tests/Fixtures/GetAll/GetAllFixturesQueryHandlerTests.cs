using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetAll;

public class GetAllFixturesQueryHandlerTests
{
    private static readonly List<Competition> BaseCompetitions =
    [
        FixtureFixtures.CompetitionLeague2024_2025,
        FixtureFixtures.CompetitionCup2024_2025,
    ];

    private static readonly List<Fixture> BaseFixtures =
    [
        FixtureFixtures.FixtureGW1League2024_2925,
        FixtureFixtures.FixtureR1Cup2024_2925
    ];

    private static readonly GetAllFixturesQuery BaseCommand = new();

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetAllFixturesQueryHandler _handler;

    public GetAllFixturesQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(BaseCompetitions.BuildMockDbSet().Object);

        _handler = new GetAllFixturesQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var competition = BaseCompetitions[0];
        var fixtures = BaseFixtures;
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions.First(competition => fixture.CompetitionId == competition.Id))).ToList());

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
        var competition = BaseCompetitions[0];
        var fixtures = BaseFixtures.Take(1).ToList();
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions.First(competition => fixture.CompetitionId == competition.Id))).ToList());

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
        var competition = BaseCompetitions[0];
        var fixtures = BaseFixtures.Take(0).ToList();
        var expected = Result.Success(fixtures.Select(fixture => fixture.ToResponse(BaseCompetitions.First(competition => fixture.CompetitionId == competition.Id))).ToList());

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
