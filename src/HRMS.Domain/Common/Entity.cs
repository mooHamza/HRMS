public abstract class Entity
{
    public Guid Id { get;} = Guid.NewGuid();

    protected Entity() { }

    protected Entity(Guid id)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
    }
}