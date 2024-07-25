using System.ComponentModel.DataAnnotations;
using Domain.Constants;
namespace Application.Users.Commands.AddUser;

public sealed record AddUserRequest
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Title { get; set; }
    [Required]
    [RegularExpression(Validation.Regex.PASSWORD)]
    public string Password { get; set; }
    [Required] public List<string> SecurityGroupsNames { get; set; }
   
    public bool Active { get; set; }
  
    [Required] public string JobTypeId { get; set; }

}
