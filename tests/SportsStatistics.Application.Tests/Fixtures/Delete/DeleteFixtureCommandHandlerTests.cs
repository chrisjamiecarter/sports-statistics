using SportsStatistics.Application.Fixtures;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Delete;

public class DeleteFixtureCommandHandlerTests
{
    private static readonly Competition BaseCompetition = Competition.Create("Test League", CompetitionType.League);
    private static readonly Fixture BaseFixture = Fixture.Create(DateTime.UtcNow, BaseCompetition, FixtureLocation.Home);
    private static readonly DeleteFixtureCommand BaseCommand = new(BaseFixture.Id.Value);

    private readonly Mock<IFixtureRepository> _repositoryMock;
    private readonly DeleteFixtureCommandHandler _handler;

    public DeleteFixtureCommandHandlerTests()
    {
        _repositoryMock = new Mock<IFixtureRepository>();
        _handler = new DeleteFixtureCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseFixture);

        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(FixtureErrors.NotFound(BaseFixture.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Fixture?)null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(FixtureErrors.NotDeleted(BaseFixture.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseFixture);

        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
