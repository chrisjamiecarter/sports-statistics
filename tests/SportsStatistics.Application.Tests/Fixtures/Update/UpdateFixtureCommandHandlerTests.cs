using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Update;

public class UpdateFixtureCommandHandlerTests
{
    private static readonly List<Competition> BaseCompetitions =
    [
        Competition.Create(EntityId.Create(), "Test League", CompetitionType.League.Name),
        Competition.Create(EntityId.Create(), "Test Cup", CompetitionType.Cup.Name),
    ];

    private static readonly Fixture BaseFixture = Fixture.Create(BaseCompetitions.First().Id,
                                                                 "Test Opponent",
                                                                 DateTime.UtcNow.AddDays(7),
                                                                 FixtureLocation.Home.Name);

    private static readonly UpdateFixtureCommand BaseCommand = new(BaseFixture.Id.Value,
                                                                   BaseFixture.Opponent,
                                                                   BaseFixture.KickoffTimeUtc.AddDays(1),
                                                                   FixtureLocation.Neutral.Name);

    private readonly Mock<DbSet<Competition>> _competitionDbSetMock;
    private readonly Mock<DbSet<Fixture>> _fixtureDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateFixtureCommandHandler _handler;

    public UpdateFixtureCommandHandlerTests()
    {
        _competitionDbSetMock = BaseCompetitions.BuildMockDbSet();
        _fixtureDbSetMock = new List<Fixture>([BaseFixture]).BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitionDbSetMock.Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(_fixtureDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateFixtureCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { Id = Guid.CreateVersion7() };
        var expected = Result.Failure(FixtureErrors.NotFound(EntityId.Create(command.Id)));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(FixtureErrors.NotUpdated(EntityId.Create(command.Id)));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
