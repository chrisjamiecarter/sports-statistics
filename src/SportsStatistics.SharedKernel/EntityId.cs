namespace SportsStatistics.SharedKernel;

public readonly record struct EntityId
{
    private static Error IncorrectVersion => Error.Failure(
        "EntityId.IncorrectVersion",
        "ID values must be version 7");

    private EntityId(Guid value)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Version, 7, nameof(value));

        Value = value;
    }

    public Guid Value { get; }

    public static Result<EntityId> Create()
    {
        return new EntityId(Guid.CreateVersion7());
    }

    public static Result<EntityId> FromValue(Guid value)
    {
        return value.Version == 7
            ? new EntityId(value)
            : Result.Failure<EntityId>(IncorrectVersion);
    }

    public override string ToString() => Value.ToString();
}
