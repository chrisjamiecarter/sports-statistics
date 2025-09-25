using SportsStatistics.Application.Competitions;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.Update;

public class UpdateCompetitionCommandHandlerTests
{
    private static readonly Competition BaseCompetition = Competition.Create("Test Competition", CompetitionType.League);
    private static readonly UpdateCompetitionCommand BaseCommand = new(BaseCompetition.Id.Value, "Updated Name", CompetitionType.Cup.Name);

    private readonly Mock<ICompetitionRepository> _repositoryMock;
    private readonly UpdateCompetitionCommandHandler _handler;

    public UpdateCompetitionCommandHandlerTests()
    {
        _repositoryMock = new Mock<ICompetitionRepository>();
        _handler = new UpdateCompetitionCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseCompetition);

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Once);
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
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(CompetitionErrors.NotUpdated(BaseCompetition.Id));

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(BaseCompetition);

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<EntityId>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Competition>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
