using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class InvalidSquadNumberTestCase : TheoryData<int?, Error>
{
    public InvalidSquadNumberTestCase()
    {
        Add(SquadNumberTestData.NullSquadNumber, PlayerErrors.SquadNumber.NullOrEmpty);
        Add(SquadNumberTestData.BelowMinValueSquadNumber, PlayerErrors.SquadNumber.BelowMinValue);
        Add(SquadNumberTestData.AboveMaxValueSquadNumber, PlayerErrors.SquadNumber.AboveMaxValue);
    }
}
