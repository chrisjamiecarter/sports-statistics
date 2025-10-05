using SportsStatistics.Application.Fixtures;
using SportsStatistics.Application.Fixtures.GetAll;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetAll;

public class GetFixturesQueryHandlerTests
{
    private static readonly Competition BaseCompetition = Competition.Create("Test League", CompetitionType.League);
    private static readonly GetFixturesQuery BaseCommand = new();

    private readonly Mock<IFixtureRepository> _repositoryMock;
    private readonly GetFixturesQueryHandler _handler;

    public GetFixturesQueryHandlerTests()
    {
        _repositoryMock = new Mock<IFixtureRepository>();
        _handler = new GetFixturesQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var fixtures = new List<Fixture>
        {
            Fixture.Create(DateTime.UtcNow.AddDays(7), BaseCompetition, FixtureLocation.Home),
            Fixture.Create(DateTime.UtcNow.AddDays(14), BaseCompetition, FixtureLocation.Away),
        };
        var expected = Result.Success(fixtures.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(fixtures);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenOne()
    {
        // Arrange.
        var command = BaseCommand;
        var fixtures = new List<Fixture>
        {
            Fixture.Create(DateTime.UtcNow.AddDays(7), BaseCompetition, FixtureLocation.Home),
        };
        var expected = Result.Success(fixtures.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(fixtures);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNone()
    {
        // Arrange.
        var command = BaseCommand;
        var fixtures = new List<Fixture>();
        var expected = Result.Success(fixtures.ToResponse());

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(fixtures);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
