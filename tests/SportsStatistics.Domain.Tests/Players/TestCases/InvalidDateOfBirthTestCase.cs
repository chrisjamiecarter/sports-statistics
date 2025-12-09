using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class InvalidDateOfBirthTestCase : TheoryData<DateOnly?, Error>
{
    public InvalidDateOfBirthTestCase()
    {
        Add(DateOfBirthTestData.NullDateOfBirth, PlayerErrors.DateOfBirth.NullOrEmpty);
        Add(DateOfBirthTestData.EmptyDateOfBirth, PlayerErrors.DateOfBirth.NullOrEmpty);
        Add(DateOfBirthTestData.YoungerThanAllowedDateOfBirth, PlayerErrors.DateOfBirthBelowMinAge);
    }
}
