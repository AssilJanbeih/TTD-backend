namespace Application.Users.Queries.GetUsers;

public sealed record GetUsersResponse(string Id,string Name, string Email, string Title, string IsActive, string JobType );