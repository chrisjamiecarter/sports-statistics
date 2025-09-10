using SportsStatistics.Domain.Primitives;

namespace SportsStatistics.Domain.Entities;

public sealed class Player : AggregateRoot
{
    public Player(Guid id, string name, string role, int squadNumber, string nationality, DateOnly dateOfBirth) : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        ArgumentException.ThrowIfNullOrWhiteSpace(role, nameof(role));

        ArgumentOutOfRangeException.ThrowIfNegative(squadNumber, nameof(squadNumber));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(squadNumber, 99, nameof(squadNumber));

        ArgumentException.ThrowIfNullOrWhiteSpace(nationality, nameof(nationality));

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(dateOfBirth, DateOnly.FromDateTime(DateTime.Today), nameof(dateOfBirth));

        Name = name;
        Role = role;
        SquadNumber = squadNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
    }
        
    public string Name { get; private set; }

    /// <summary>
    /// e.g., "Goalkeeper", "Defender", "Midfielder", "Attacker").
    /// </summary>
    public string Role { get; private set; }

    public int SquadNumber { get; private set; }

    public string Nationality { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public int Age
    {
        get
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - DateOfBirth.Year;
            
            if (DateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
