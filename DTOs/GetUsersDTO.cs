namespace TTD_Backend.DTOs;
public class GetUsersDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public bool Active { get; set; }
    public string Role { get; set; }
}
