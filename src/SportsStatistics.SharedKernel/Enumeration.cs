namespace SportsStatistics.SharedKernel;

public abstract class Enumeration(int id, string name) : IComparable<Enumeration>
{
    public int Id { get; } = id;

    public string Name { get; } = name;

    public static bool operator ==(Enumeration? a, Enumeration? b)
    {
        return a is not null && b is not null && a.Equals(b);
    }

    public static bool operator !=(Enumeration? a, Enumeration? b)
    {
        return !(a == b);
    }

    public static bool operator <(Enumeration? a, Enumeration? b)
    {
        return a is not null && b is not null && a < b;
    }

    public static bool operator <=(Enumeration? a, Enumeration? b)
    {
        return a is not null && b is not null && a <= b;
    }

    public static bool operator >(Enumeration? a, Enumeration? b)
    {
        return a is not null && b is not null && a > b;
    }

    public static bool operator >=(Enumeration? a, Enumeration? b)
    {
        return a is not null && b is not null && a >= b;
    }

    public int CompareTo(Enumeration? other)
    {
        return other == null ? 1 : Id.CompareTo(other.Id);
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Enumeration enumeration)
        {
            return false;
        }

        return enumeration.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString() => Name;
}
