using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Competition : Entity
{
    private Competition(Guid seasonId,
                        Name name,
                        Format format)
        : base(Guid.CreateVersion7())
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

    internal static Competition Create(Season season, Name name, Format format)
    {
        return new(season.Id, name, format);
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
        //Raise(new CompetitionNameChangedDomainEvent(this, previousName));

        return true;
    }

    public bool ChangeFormat(Format format)
    {
        if (Format == format)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousFormat = Format;
        Format = format;
        //Raise(new CompetitionFormatChangedDomainEvent(this, previousFormat));

        return true;
    }
}
