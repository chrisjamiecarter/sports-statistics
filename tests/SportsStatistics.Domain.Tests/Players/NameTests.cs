using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_ShouldReturnFailureResult_WhenNameIsNullOrEmpty(string? name)
    {
        // Arrange.
        var expected = Result.Failure<Name>(PlayerErrors.Name.NullOrEmpty);

        // Act.
        var result = Name.Create(name);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }

    [Fact]
    public void Create_ShouldReturnFailureResult_WhenNameIsTooLong()
    {
        // Arrange.
        var name = NameTestData.LongerThanAllowedName;
        var expected = Result.Failure<Name>(PlayerErrors.Name.ExceedsMaxLength);

        // Act.
        var result = Name.Create(name);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected.Error);
    }
}
