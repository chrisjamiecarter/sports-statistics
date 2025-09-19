using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed class Player //: Entity
{
    //private Player(EntityId id) : base(id)
    //{
    //    // EF Core.
    //}

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // TODO: player may not have a squad number assigned.
    public int SquadNumber { get; set; }

    public string Nationality { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public Position Position { get; set; } = Position.Unknown;

    public int Age => DateOfBirth.CalculateAge();

    public void Update(string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position)
    {
        Name = name;
        SquadNumber = squadNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
        Position = position;
    }
}
