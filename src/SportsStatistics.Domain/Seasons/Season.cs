using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed class Season : Entity
{
    private Season(DateRange dateRange)
        : base(Guid.CreateVersion7())
    {
        DateRange = dateRange;
    }

    public DateRange DateRange { get; private set; }

    public string Name => $"{DateRange.StartDate.Year}/{DateRange.EndDate.Year}";

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
}
