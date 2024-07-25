namespace Application.Users.Queries.GetUser;

public sealed record GetUserResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }    
    public string Email { get; set; }
    public bool Active { get; set; }
    public string JobTypeId { get; set; }
    

}