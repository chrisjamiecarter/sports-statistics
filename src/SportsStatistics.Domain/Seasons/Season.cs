using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed class Season : Entity
{
    private Season(EntityId id, DateOnly startDate, DateOnly endDate) : base(id)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public string Name => $"{StartDate.Year}/{EndDate.Year}";

    public static Season Create(DateOnly startDate, DateOnly endDate)
    {
        ValidateAndThrow(startDate, endDate);

        return new Season(EntityId.Create(), startDate, endDate);
    }

    public void Update(DateOnly startDate, DateOnly endDate)
    {
        ValidateAndThrow(startDate, endDate);

        StartDate = startDate;
        EndDate = endDate;
    }

    private static void ValidateAndThrow(DateOnly startDate, DateOnly endDate)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startDate, endDate, nameof(startDate));
    }
}
