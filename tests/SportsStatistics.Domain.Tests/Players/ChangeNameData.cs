using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players;

public class ChangeNameData : TheoryData<Name, Name>
{
    public ChangeNameData() => Add(Name.Create("Old Name").Value, Name.Create("New Name").Value);
}
