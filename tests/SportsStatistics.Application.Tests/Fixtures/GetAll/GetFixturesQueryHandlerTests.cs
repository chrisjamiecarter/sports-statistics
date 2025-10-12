using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetAll;

public class GetFixturesQueryHandlerTests
{
    private static readonly GetFixturesQuery BaseCommand = new();

    private static readonly Competition BaseCompetition = Competition.Create("Test Competition", CompetitionType.League);

    private static readonly List<Fixture> BaseFixtures =
    [
            Fixture.Create(BaseCompetition.Id, "Test Opponent", DateTime.UtcNow.AddDays(7), FixtureLocation.Home),
            Fixture.Create(BaseCompetition.Id, "Test Opponent", DateTime.UtcNow.AddDays(14), FixtureLocation.Away),
    ];

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetFixturesQueryHandler _handler;

    public GetFixturesQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        _handler = new GetFixturesQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var competitions = new List<Competition>() { BaseCompetition };
        var fixtures = BaseFixtures;
        var expected = Result.Success(fixtures.Select(f => f.ToResponse(BaseCompetition)));

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
        var competitions = new List<Competition>() { BaseCompetition };
        var fixtures = BaseFixtures.Take(1).ToList();
        var expected = Result.Success(fixtures.Select(f => f.ToResponse(BaseCompetition)));

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
        var competitions = new List<Competition>() { BaseCompetition };
        var fixtures = BaseFixtures.Take(0).ToList();
        var expected = Result.Success(fixtures.Select(f => f.ToResponse(BaseCompetition)));

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
