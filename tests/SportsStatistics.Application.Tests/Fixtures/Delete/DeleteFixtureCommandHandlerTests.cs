using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Delete;

public class DeleteFixtureCommandHandlerTests
{
    private static readonly List<Fixture> BaseFixtures =
    [
        Fixture.Create(EntityId.Create(), "Test Opponent", DateTime.UtcNow, FixtureLocation.Home.Name),
    ];

    private static readonly DeleteFixtureCommand BaseCommand = new(BaseFixtures[0].Id);

    private readonly Mock<DbSet<Fixture>> _fixtureDbSetMock;
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly DeleteFixtureCommandHandler _handler;

    public DeleteFixtureCommandHandlerTests()
    {
        _fixtureDbSetMock = BaseFixtures.BuildMockDbSet();

        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(_fixtureDbSetMock.Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new DeleteFixtureCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsDeleted()
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
        var command = BaseCommand with { FixtureId = Guid.CreateVersion7() };
        var expected = Result.Failure(FixtureErrors.NotFound(command.FixtureId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotDeleted()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Failure(FixtureErrors.NotDeleted(command.FixtureId));

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(0);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
