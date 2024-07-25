namespace Domain.Entities.Base;

public class Entity
{
    protected Entity(Guid id) => Id = id;

    protected Entity()
    {
    }
    public Guid Id { get; set; } = Guid.NewGuid();
}