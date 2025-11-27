using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed class Season : Entity, ISoftDeletableEntity
{
    private Season(DateRange dateRange)
        : base(Guid.CreateVersion7())
    {
        DateRange = dateRange;
    }

    public DateRange DateRange { get; private set; }

    public string Name => $"{DateRange.StartDate.Year}/{DateRange.EndDate.Year}";

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }

    public static Season Create(DateRange dateRange)
    {
        return new Season(dateRange);
    }
        
    public bool ChangeDateRange(DateRange dateRange)
    {
        if (DateRange == dateRange)
        {
            return false;
        }

        // TODO: Raise Domain Event.
        //string previousDateRange = DateRange;
        DateRange = dateRange;
        //Raise(new SeasonDateRangeChangedDomainEvent(this, previousDateRange));

        return true;
    }

    public Competition CreateCompetition(Name name, Format format)
    {
        return Competition.Create(this, name, format);
    }
}
