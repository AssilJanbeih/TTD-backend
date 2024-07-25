using Domain.Entities.Base;
using Domain.Entities.Identity;
using System;
using System.Xml.Linq;

namespace Domain.Entities;

public class Task : BaseAuditableEntity, IActivityAuditingEntity
{
    private Task()
    {
    }

    private Task(string name, string projectId,string assignee, string status,DateTime startDate,DateTime estimatedEndDate, DateTime? actualEndDate)
    {
        Name = name;
        ProjectId = projectId;
        AssigneeId =  assignee;
        Status = status;
        StartDate = startDate;
        EstimatedEndDate = estimatedEndDate;
        ActualEndDate = actualEndDate;

    }

    public void Update(string name, string projectId, string assignee, string status, DateTime startDate, DateTime estimatedEndDate, DateTime? actualEndDate)
    {
        Name = name;
        ProjectId = projectId;
        AssigneeId = assignee;
        Status = status;
        StartDate = startDate;
        EstimatedEndDate = estimatedEndDate;
        ActualEndDate = actualEndDate;

    }

    public static Task Create(string name, string projectId, string assignee, string status, DateTime startDate, DateTime estimatedEndDate, DateTime? actualEndDate)
    {
        var task = new Task(name, projectId, assignee, status, startDate, estimatedEndDate, actualEndDate);


        return task;
    }



    public string Name { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EstimatedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public string ProjectId { get; set; }
    public Project Project { get; set; }

    public string AssigneeId { get; set; }
    public User Assignee { get; set; }


    public string GetLogIdentifier()
    {
        return Name;
    }
}