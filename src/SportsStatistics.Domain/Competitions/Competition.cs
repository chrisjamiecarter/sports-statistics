using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Competition : Entity
{
    private Competition(EntityId id, string name, CompetitionType type) : base(id)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; set; } = string.Empty;

    public CompetitionType Type { get; set; } = CompetitionType.Unknown;

    public static Competition Create(string name, CompetitionType type)
    {
        Validate(name, type);

        return new Competition(EntityId.Create().Value, name, type);
    }

    public void Update(string name, CompetitionType type)
    {
        Validate(name, type);

        Name = name;
        Type = type;
    }

    private static void Validate(string name, CompetitionType type)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentNullException.ThrowIfNull(type);
        if (type == CompetitionType.Unknown)
        {
            throw new ArgumentException("A competition cannot have a type of unknown.");
        }
    }
}
