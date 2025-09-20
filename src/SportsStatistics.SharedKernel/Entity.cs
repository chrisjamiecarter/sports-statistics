namespace SportsStatistics.SharedKernel;

public abstract class Entity(EntityId id)
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public EntityId Id { get; } = id;

    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
