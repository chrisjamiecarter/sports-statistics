using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Competition : Entity
{
    private Competition(EntityId id, EntityId seasonId, string name, CompetitionType type) : base(id)
    {
        SeasonId = seasonId;
        Name = name;
        Type = type;
    }

    public EntityId SeasonId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public CompetitionType Type { get; private set; } = CompetitionType.Unknown;

    public static Competition Create(EntityId seasonId, string name, string competitionTypeName)
    {
        var competitionType = CompetitionType.FromName(competitionTypeName);

        ValidateAndThrow(name, competitionType);

        return new Competition(EntityId.Create(), seasonId, name, competitionType);
    }

    public void Update(string name, string competitionTypeName)
    {
        var competitionType = CompetitionType.FromName(competitionTypeName);

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
