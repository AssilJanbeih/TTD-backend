using System;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain;


public class User : IdentityUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public bool Active { get; set; }
    public int? JobTypeId { get; set; }
    public JobType? JobType { get; set; }
    public DateTime? CreatedAt { get; set; }

}