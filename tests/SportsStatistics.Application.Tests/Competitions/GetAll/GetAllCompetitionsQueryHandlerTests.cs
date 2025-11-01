using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions.GetAll;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.GetAll;

public class GetAllCompetitionsQueryHandlerTests
{
    private static readonly Season BaseSeason = Season.Create(new DateOnly(2025, 8, 1),
                                                              new DateOnly(2026, 7, 31));

    private static readonly List<Competition> BaseCompetitions =
    [
        Competition.Create(BaseSeason.Id, "Test League", CompetitionType.League.Name),
        Competition.Create(BaseSeason.Id, "Test Cup", CompetitionType.Cup.Name),
    ];

    private static readonly GetAllCompetitionsQuery BaseCommand = new(BaseSeason.Id);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetAllCompetitionsQueryHandler _handler;

    public GetAllCompetitionsQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _handler = new GetAllCompetitionsQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var competitions = BaseCompetitions;
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
        var competitions = BaseCompetitions.Take(1).ToList();
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
        var competitions = BaseCompetitions.Take(0).ToList();
        var expected = Result.Success(competitions.ToResponse());

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(competitions.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
