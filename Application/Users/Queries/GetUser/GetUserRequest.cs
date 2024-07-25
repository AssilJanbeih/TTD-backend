namespace Application.Users.Queries.GetUser;

public sealed record GetUserRequest
{ 
    public string UserEntityId { get; set; }
}