using Domain.Entities.Base;
using System.Collections.Generic;

namespace Domain.Entities.Identity;

public class JobType : BaseEntity
{
    public JobType(int id,string name, string entityId): base(id, entityId)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public List<User> Users { get; set; }
}