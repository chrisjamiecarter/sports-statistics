using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions.GetAll;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.GetAll;

public class GetCompetitionsQueryHandlerTests
{
    private static readonly GetAllCompetitionsQuery BaseCommand = new();

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetAllCompetitionsQueryHandler _handler;

    public GetCompetitionsQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _handler = new GetAllCompetitionsQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var competitions = new List<Competition>
        {
            Competition.Create(EntityId.Create(), "Test League", CompetitionType.League.Name),
            Competition.Create(EntityId.Create(), "Test Cup", CompetitionType.Cup.Name),
        };
        var expected = Result.Success(competitions.ToResponse());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

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
        var competitions = new List<Competition>
        {
            Competition.Create(EntityId.Create(), "Test League", CompetitionType.League.Name),
        };
        var expected = Result.Success(competitions.ToResponse());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

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
        var competitions = new List<Competition>();
        var expected = Result.Success(competitions.ToResponse());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
