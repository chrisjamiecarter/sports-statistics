using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed class Season : Entity
{
    private Season(Guid id) : base(new EntityId(id))
    {
    }

    private Season(EntityId id, DateOnly startDate, DateOnly endDate, string? customDisplayName) : this(id.Value)
    {
        StartDate = startDate;
        EndDate = endDate;
        CustomDisplayName = customDisplayName;
    }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public string Name
        => !string.IsNullOrWhiteSpace(CustomDisplayName)
        ? CustomDisplayName
        : $"{StartDate.Year}/{EndDate.Year}";

    private string? CustomDisplayName { get; set; }

    public bool IsCurrent(DateOnly referenceDate)
        => referenceDate >= StartDate
        && referenceDate <= EndDate;

    public static Season Create(DateOnly startDate, DateOnly endDate, string? customDisplayName = null)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startDate, endDate, nameof(startDate));

        return new Season(EntityId.Create(), startDate, endDate, customDisplayName);
    }

    public void Update(DateOnly startDate, DateOnly endDate, string? customDisplayName = null)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(startDate, endDate, nameof(startDate));

        StartDate = startDate;
        EndDate = endDate;
        CustomDisplayName = customDisplayName;
    }
}
