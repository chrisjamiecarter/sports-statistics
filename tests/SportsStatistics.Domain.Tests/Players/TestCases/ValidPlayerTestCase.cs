using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ValidPlayerTestCase : TheoryData<Name, SquadNumber, Nationality, DateOfBirth, Position, int>
{
    public ValidPlayerTestCase()
    {
        Add(NameTestData.ValidName,
            SquadNumberTestData.ValidSquadNumber,
            NationalityTestData.ValidNationality,
            DateOfBirthTestData.ValidDateOfBirth,
            PositionTestData.ValidPosition,
            DateOfBirthTestData.ValidDateOfBirth.Value.CalculateAge());
    }
}
