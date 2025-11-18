using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Competition : Entity
{
    private Competition(Guid seasonId, Name name, Format format) : base(Guid.CreateVersion7())
    {
        SeasonId = seasonId;
        Name = name;
        Format = format;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Competition"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Competition() { }

    public Guid SeasonId { get; private set; } = default!;

    public Name Name { get; private set; } = default!;

    public Format Format { get; private set; } = default!;

    public static Competition Create(Guid seasonId, Name name, Format format)
    {
        return new Competition(seasonId, name, format);
    }

    public void ChangeName(Name name)
    {
        Name = name;
    }

    public void ChangeFormat(Format format)
    {
        Format = format;
    }
}
