namespace SportsStatistics.SharedKernel;

public readonly record struct EntityId
{
    private static Error IncorrectVersion => Error.Failure(
        "EntityId.IncorrectVersion",
        "ID values must be version 7");

    private EntityId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static Result<EntityId> Create()
    {
        return new EntityId(Guid.CreateVersion7());
    }

    public static Result<EntityId> Create(Guid value)
    {
        return TryParse(value, out var id)
            ? Result.Success(id)
            : Result.Failure<EntityId>(IncorrectVersion);
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

    public override string ToString() => Value.ToString();
}
