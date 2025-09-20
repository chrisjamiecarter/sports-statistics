namespace SportsStatistics.SharedKernel;

public readonly record struct EntityId
{
    public EntityId(Guid value)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Version, 7, nameof(value));
        
        Value = value;
    }

    public Guid Value { get; }

    public static EntityId Create() => new(Guid.CreateVersion7());
    
    public override string ToString() => Value.ToString();
}
