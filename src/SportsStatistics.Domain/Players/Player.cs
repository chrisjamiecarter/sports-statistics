using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed class Player : Entity
{
    private Player(Name name,
                   SquadNumber squadNumber,
                   Nationality nationality,
                   DateOfBirth dateOfBirth,
                   Position position)
        : base(Guid.CreateVersion7())
    {
        Name = name;
        SquadNumber = squadNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
        Position = position;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Player() { }

    public Name Name { get; private set; } = default!;

    public SquadNumber SquadNumber { get; private set; } = default!;

    public Nationality Nationality { get; private set; } = default!;

    public DateOfBirth DateOfBirth { get; private set; } = default!;

    public Position Position { get; private set; } = default!;

    public int Age => DateOfBirth.Value.CalculateAge();

    public static Player Create(Name name, SquadNumber squadNumber, Nationality nationality, DateOfBirth dateOfBirth, Position position)
    {
        return new Player(name, squadNumber, nationality, dateOfBirth, position);
    }

    public bool ChangeName(Name name)
    {
        if (Name == name)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousName = Name;
        Name = name;
        //Raise(new PlayerNameChangedDomainEvent(this, previousName));

        return true;
    }

    public bool ChangeSquadNumber(SquadNumber squadNumber)
    {
        if (SquadNumber == squadNumber)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousSquadNumber = SquadNumber;
        SquadNumber = squadNumber;
        //Raise(new PlayerSquadNumberChangedDomainEvent(this, previousSquadNumber));

        return true;
    }

    public bool ChangeNationality(Nationality nationality)
    {
        if (Nationality == nationality)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousNationality = Nationality;
        Nationality = nationality;
        //Raise(new PlayerNationalityChangedDomainEvent(this, previousNationality));

        return true;
    }

    public bool ChangeDateOfBirth(DateOfBirth dateOfBirth)
    {
        if (DateOfBirth == dateOfBirth)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousDateOfBirth = DateOfBirth;
        DateOfBirth = dateOfBirth;
        //Raise(new PlayerDateOfBirthChangedDomainEvent(this, previousDateOfBirth));

        return true;
    }

    public bool ChangePosition(Position position)
    {
        if (Position == position)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousPosition = Position;
        Position = position;
        //Raise(new PlayerPositionChangedDomainEvent(this, previousPosition));

        return true;
    }

    //private static void ValidateAndThrow(string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position)
    //{
    //    ArgumentException.ThrowIfNullOrEmpty(name);
    //    ArgumentOutOfRangeException.ThrowIfLessThan(squadNumber, 1, nameof(squadNumber));
    //    ArgumentException.ThrowIfNullOrEmpty(nationality);
    //    ArgumentOutOfRangeException.ThrowIfGreaterThan(dateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-15)), nameof(dateOfBirth));
    //    if (position == Position.Unknown)
    //    {
    //        throw new ArgumentException("A player cannot have a position of unknown.", nameof(position));
    //    }
    //}
}
