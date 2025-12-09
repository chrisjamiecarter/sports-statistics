using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class DateOfBirthTests
{
    [Fact]
    public void Create_ShouldReturnFailureResult_WhenDateOfBirthIsNull()
    {
        // Arrange.
        var expected = Result.Failure<Name>(PlayerErrors.DateOfBirth.NullOrEmpty);

        // Act.
        var result = DateOfBirth.Create(null);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }

    [Fact]
    public void Create_ShouldReturnFailureResult_WhenDateOfBirthIsEmpty()
    {
        // Arrange.
        var expected = Result.Failure<Name>(PlayerErrors.DateOfBirth.NullOrEmpty);

        // Act.
        var result = DateOfBirth.Create(DateOnly.MinValue);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }

    [Fact]
    public void Create_ShouldReturnFailureResult_WhenDateOfBirthIsTooYoung()
    {
        // Arrange.
        var dateOfBirth = DateOfBirthTestData.YoungerThanAllowedDateOfBirth;
        var expected = Result.Failure<Name>(PlayerErrors.DateOfBirthBelowMinAge);

        // Act.
        var result = DateOfBirth.Create(dateOfBirth);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }
}
