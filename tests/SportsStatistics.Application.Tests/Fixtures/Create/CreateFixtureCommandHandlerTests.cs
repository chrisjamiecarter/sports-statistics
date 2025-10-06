using SportsStatistics.Application.Competitions;
using SportsStatistics.Application.Fixtures;
using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Create;

public class CreateFixtureCommandHandlerTests
{
    private static readonly CreateFixtureCommand BaseCommand = new(Guid.CreateVersion7(),
                                                                   DateTime.UtcNow,
                                                                   FixtureLocation.Home.Name);

    private static readonly Competition BaseCompetition = Competition.Create("Test Competition", CompetitionType.League);

    private readonly Mock<IFixtureRepository> _fixtureRepositoryMock;
    private readonly Mock<ICompetitionRepository> _competitionRepositoryMock;

    private readonly CreateFixtureCommandHandler _handler;

    public CreateFixtureCommandHandlerTests()
    {
        _fixtureRepositoryMock = new Mock<IFixtureRepository>();
        _competitionRepositoryMock = new Mock<ICompetitionRepository>();
        _handler = new CreateFixtureCommandHandler(_fixtureRepositoryMock.Object, _competitionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsCreated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _competitionRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(BaseCompetition);

        _fixtureRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _competitionRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _fixtureRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureLocationIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { FixtureLocation = "The Moon" };
        var expected = Result.Failure(FixtureErrors.InvalidLocation(command.FixtureLocation));

        _competitionRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(BaseCompetition);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _competitionRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _fixtureRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(CompetitionErrors.NotFound(EntityId.Create(command.CompetitionId)));

        _competitionRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync((Competition?)null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _competitionRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _fixtureRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotCreated()
    {
        // Arrange.
        var command = BaseCommand;
        Result? expected = null; // Set in the callback below.

        _competitionRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(BaseCompetition);

        _fixtureRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()))
                              .Callback<Fixture, CancellationToken>((c, _) =>
                              {
                                  expected = Result.Failure(FixtureErrors.NotCreated(c.Id));
                              })
                              .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _competitionRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _fixtureRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
