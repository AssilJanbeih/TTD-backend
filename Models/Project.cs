
namespace Models;

public class Project 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ClientEmail { get; set; }
    public string SlackLink { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EstimatedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public Decimal? Completion { get; set; }
    public string ProjectManagerId { get; set; }
    public User ProjectManager { get; set; }

    public List<Task> Tasks { get; set; }

    public void UpdateCompletion()
    {
        int completionSum = 0;
        foreach (var contract in Tasks)
        {
            if (contract.Status.Equals("Done")) {
                completionSum ++;
            } }

        Completion = completionSum / Tasks.Count;
    }
}