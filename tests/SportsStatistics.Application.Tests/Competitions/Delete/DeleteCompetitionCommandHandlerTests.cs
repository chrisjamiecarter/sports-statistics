using SportsStatistics.Application.Competitions;
using SportsStatistics.Application.Competitions.Delete;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.Delete;

public class DeleteCompetitionCommandHandlerTests
{
    private static readonly Competition BaseCompetition = Competition.Create("Test Competition", CompetitionType.League);
    private static readonly DeleteCompetitionCommand BaseCommand = new(BaseCompetition.Id.Value);

    private readonly Mock<ICompetitionRepository> _repositoryMock;
    private readonly DeleteCompetitionCommandHandler _handler;

    public DeleteCompetitionCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICompetitionRepository>();
        _handler = new DeleteCompetitionCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseCompetition);

        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotFound()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(CompetitionErrors.NotFound(BaseCompetition.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Competition?)null);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(CompetitionErrors.NotDeleted(BaseCompetition.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseCompetition);

        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
