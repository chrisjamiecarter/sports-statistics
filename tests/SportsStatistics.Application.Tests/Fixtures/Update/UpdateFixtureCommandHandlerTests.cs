//using SportsStatistics.Application.Competitions;
//using SportsStatistics.Application.Fixtures;
//using SportsStatistics.Application.Fixtures.Update;
//using SportsStatistics.Domain.Competitions;
//using SportsStatistics.Domain.Fixtures;
//using SportsStatistics.SharedKernel;

//namespace SportsStatistics.Application.Tests.Fixtures.Update;

//public class UpdateFixtureCommandHandlerTests
//{
//    private static readonly Fixture BaseFixture = Fixture.Create(EntityId.Create(), "Test Opponent", DateTime.UtcNow.AddDays(7), FixtureLocation.Home);
//    private static readonly UpdateFixtureCommand BaseCommand = new(BaseFixture.Id.Value, BaseFixture.Opponent, BaseFixture.KickoffTimeUtc.AddDays(1), FixtureLocation.Neutral.Name);

//    private readonly Mock<IFixtureRepository> _repositoryMock;
//    private readonly UpdateFixtureCommandHandler _handler;

//    public UpdateFixtureCommandHandlerTests()
//    {
//        _repositoryMock = new Mock<IFixtureRepository>();
//        _handler = new UpdateFixtureCommandHandler(_repositoryMock.Object);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsUpdated()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var expected = Result.Success();

//        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(BaseFixture);

//        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(true);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
//        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Once);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotFound()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var expected = Result.Failure(FixtureErrors.NotFound(BaseFixture.Id));

//        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
//               .ReturnsAsync((Fixture?)null);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
//        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Never);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotUpdated()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var expected = Result.Failure(FixtureErrors.NotUpdated(BaseFixture.Id));

//        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(BaseFixture);

//        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(false);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
//        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Fixture>(), It.IsAny<CancellationToken>()), Times.Once);
//    }
//}
