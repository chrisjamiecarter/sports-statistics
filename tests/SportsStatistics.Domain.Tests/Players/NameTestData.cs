using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players;

public class NameTestData
{
    public static readonly Name ValidName = Name.Create(nameof(Name)).Value;

    public static readonly string LongerThanAllowedName = new('*', Name.MaxLength + 1);
}
