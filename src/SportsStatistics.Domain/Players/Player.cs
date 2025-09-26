using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed class Player : Entity
{
    private Player(EntityId id, string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position) : base(id)
    {
        Name = name;
        SquadNumber = squadNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
        Position = position;
    }

    public string Name { get; private set; } = string.Empty;

    // TODO: player may not have a squad number assigned.
    public int SquadNumber { get; private set; }

    public string Nationality { get; private set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }

    public Position Position { get; private set; } = Position.Unknown;

    public int Age => DateOfBirth.CalculateAge();

    public static Player Create(string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position)
    {
        ValidateAndThrow(name, squadNumber, nationality, dateOfBirth, position);

        return new(EntityId.Create(), name, squadNumber, nationality, dateOfBirth, position);
    }

    public void Update(string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position)
    {
        ValidateAndThrow(name, squadNumber, nationality, dateOfBirth, position);

        Name = name;
        SquadNumber = squadNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
        Position = position ?? throw new ArgumentNullException(nameof(position));
    }

    private static void ValidateAndThrow(string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfLessThan(squadNumber, 1, nameof(squadNumber));
        ArgumentException.ThrowIfNullOrEmpty(nationality);
        ArgumentOutOfRangeException.ThrowIfLessThan(dateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-15)), nameof(squadNumber));
        if (position == Position.Unknown)
        {
            throw new ArgumentException("A player cannot have a position of unknown.", nameof(position));
        }
    }
}
