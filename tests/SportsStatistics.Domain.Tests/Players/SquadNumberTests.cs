using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class SquadNumberTests
{
    [Theory]
    [ClassData(typeof(InvalidSquadNumberTestCase))]
    public void Create_ShouldReturnFailureResult_WhenSquadNumberIsInvalid(int? squadNumber, Error error)
    {
        // Arrange.
        var expected = Result.Failure<SquadNumber>(error);

        // Act.
        var result = SquadNumber.Create(squadNumber);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }

    [Theory]
    [ClassData(typeof(ValidSquadNumberTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenSquadNumberIsValid(int squadNumber, SquadNumber expected)
    {
        // Arrange.
        // Act.
        var result = SquadNumber.Create(squadNumber);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
