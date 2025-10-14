//using SportsStatistics.Application.Competitions;
//using SportsStatistics.Application.Competitions.Create;
//using SportsStatistics.Domain.Competitions;
//using SportsStatistics.SharedKernel;

//namespace SportsStatistics.Application.Tests.Competitions.Create;

//public class CreateCompetitionCommandHandlerTests
//{
//    private static readonly CreateCompetitionCommand BaseCommand = new("Premier League",
//                                                                       CompetitionType.League.Name);

//    private readonly Mock<ICompetitionRepository> _repositoryMock;
//    private readonly CreateCompetitionCommandHandler _handler;

//    public CreateCompetitionCommandHandlerTests()
//    {
//        _repositoryMock = new Mock<ICompetitionRepository>();
//        _handler = new CreateCompetitionCommandHandler(_repositoryMock.Object);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsCreated()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var expected = Result.Success();

//        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(true);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Once);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotCreated()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        Result? expected = null; // Set in the callback below.

//        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()))
//                       .Callback<Competition, CancellationToken>((c, _) =>
//                       {
//                           expected = Result.Failure(CompetitionErrors.NotCreated(c.Id));
//                       })
//                       .ReturnsAsync(false);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Once);
//    }
//}
