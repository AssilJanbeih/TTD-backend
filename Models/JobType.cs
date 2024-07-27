
namespace Models;

public class JobType 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<User> Users { get; set; } = new List<User>();

    public JobType( string name)
    {
        Name = name;
    }
}