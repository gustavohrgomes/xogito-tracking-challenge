namespace RetailSystem.Domain.Common;
public abstract class Entity : IEquatable<Entity>
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity() { }

    public Guid Id { get; protected set; }

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        if (other.GetType() != GetType()) return false;

        return other.Id == Id;
    }
}
