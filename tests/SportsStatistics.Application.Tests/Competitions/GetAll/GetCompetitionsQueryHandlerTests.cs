//using SportsStatistics.Application.Competitions;
//using SportsStatistics.Application.Competitions.GetAll;
//using SportsStatistics.Domain.Competitions;
//using SportsStatistics.SharedKernel;

//namespace SportsStatistics.Application.Tests.Competitions.GetAll;

//public class GetCompetitionsQueryHandlerTests
//{
//    private static readonly GetAllCompetitionsQuery BaseCommand = new();

//    private readonly Mock<ICompetitionRepository> _repositoryMock;
//    private readonly GetAllCompetitionsQueryHandler _handler;

//    public GetCompetitionsQueryHandlerTests()
//    {
//        _repositoryMock = new Mock<ICompetitionRepository>();
//        _handler = new GetAllCompetitionsQueryHandler(_repositoryMock.Object);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnSuccess_WhenMany()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var competitions = new List<Competition>
//        {
//            Competition.Create("Test League", CompetitionType.League),
//            Competition.Create("Test Cup", CompetitionType.Cup),
//        };
//        var expected = Result.Success(competitions.ToResponse());

//        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(competitions);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnSuccess_WhenOne()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var competitions = new List<Competition>
//        {
//            Competition.Create("Test League", CompetitionType.League),
//        };
//        var expected = Result.Success(competitions.ToResponse());

//        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(competitions);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
//    }

//    [Fact]
//    public async Task Handle_ShouldReturnSuccess_WhenNone()
//    {
//        // Arrange.
//        var command = BaseCommand;
//        var competitions = new List<Competition>();
//        var expected = Result.Success(competitions.ToResponse());

//        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
//                       .ReturnsAsync(competitions);

//        // Act.
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert.
//        result.ShouldBeEquivalentTo(expected);
//        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
//    }
//}
