namespace Application.Users.Commands.UpdateUser;

public sealed record UpdateUserRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }
    public string JobTypeId { get; set; }
}
