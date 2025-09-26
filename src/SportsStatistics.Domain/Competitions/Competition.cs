using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Competition : Entity
{
    private Competition(EntityId id, string name, CompetitionType type) : base(id)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; private set; } = string.Empty;

    public CompetitionType Type { get; private set; } = CompetitionType.Unknown;

    public static Competition Create(string name, CompetitionType type)
    {
        ValidateAndThrow(name, type);

        return new Competition(EntityId.Create(), name, type);
    }

    public void Update(string name, CompetitionType competitionType)
    {
        ValidateAndThrow(name, competitionType);

        Name = name;
        Type = competitionType;
    }

    private static void ValidateAndThrow(string name, CompetitionType type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        if (type == CompetitionType.Unknown)
        {
            throw new ArgumentException("A competition cannot have a type of unknown.", nameof(type));
        }
    }
}
