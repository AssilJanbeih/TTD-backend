using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public int? JobTypeId { get; set; }
        public JobType? JobType { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
