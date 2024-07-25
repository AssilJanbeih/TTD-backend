using System;

namespace Domain.Entities.Base;

public class BaseEntity
{
    public BaseEntity()
    {
        
    }

    public BaseEntity(int id, string entityId)
    {
        Id = id;
        EntityId = entityId;
    }
    public int Id { get; set; }
    public string EntityId { get; set; } = Guid.NewGuid().ToString();
}