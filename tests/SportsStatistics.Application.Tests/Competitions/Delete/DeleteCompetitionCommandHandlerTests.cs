using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions.Delete;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.Delete;

public class DeleteCompetitionCommandHandlerTests
{
    private static readonly Competition BaseCompetition = Competition.Create(EntityId.Create(),
                                                                             "Test Competition",
                                                                             CompetitionType.League.Name);

    private static readonly DeleteCompetitionCommand BaseCommand = new(BaseCompetition.Id.Value);

    private readonly Mock<DbSet<Competition>> _competitionDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly DeleteCompetitionCommandHandler _handler;

    public DeleteCompetitionCommandHandlerTests()
    {
        _competitionDbSetMock = new List<Competition>
        {
            BaseCompetition,
        }
        .BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitionDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new DeleteCompetitionCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { Id = Guid.CreateVersion7() };
        var expected = Result.Failure(CompetitionErrors.NotFound(EntityId.Create(command.Id)));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(CompetitionErrors.NotDeleted(BaseCompetition.Id));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
