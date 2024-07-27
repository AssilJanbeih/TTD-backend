
namespace Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EstimatedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public string AssigneeId { get; set; }
    public User Assignee { get; set; }


}