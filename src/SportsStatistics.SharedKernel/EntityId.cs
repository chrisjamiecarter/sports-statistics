namespace SportsStatistics.SharedKernel;

public readonly record struct EntityId(Guid Value)
{
    public static EntityId Create() => new(Guid.CreateVersion7());
}
