namespace SportsStatistics.SharedKernel;

public readonly record struct EntityId : IComparable<EntityId>
{
    private EntityId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EntityId Create()
    {
        return new EntityId(Guid.CreateVersion7());
    }

    public static EntityId Create(Guid value)
    {
        if (!TryParse(value, out var id))
        {
            throw new ArgumentException("ID values must be version 7", nameof(value));
        }

        return id;
    }

    public static EntityId Parse(string value)
    {
        return !Guid.TryParse(value, out var guid) || !TryParse(guid, out var id)
            ? throw new FormatException("ID values must be valid version 7 GUIDs.")
            : id;
    }

    public static bool TryParse(Guid value, out EntityId id)
    {
        if (value.Version != 7)
        {
            id = default;
            return false;
        }

        id = new EntityId(value);
        return true;
    }
    
    public static bool operator <(EntityId left, EntityId right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(EntityId left, EntityId right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(EntityId left, EntityId right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(EntityId left, EntityId right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static implicit operator Guid(EntityId entityId) => entityId.Value;
    
    public static explicit operator EntityId(Guid value) => Create(value);

    public int CompareTo(EntityId other)
    {
        return Value.CompareTo(other.Value);
    }

    public override string ToString() => Value.ToString();
}
