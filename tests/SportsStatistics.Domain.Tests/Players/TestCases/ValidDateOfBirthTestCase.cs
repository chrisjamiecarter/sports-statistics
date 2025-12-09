using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ValidDateOfBirthTestCase : TheoryData<DateOnly, DateOfBirth>
{
    public ValidDateOfBirthTestCase()
    {
        Add(DateOfBirthTestData.ValidDateOfBirth.Value, DateOfBirthTestData.ValidDateOfBirth);
    }
}
