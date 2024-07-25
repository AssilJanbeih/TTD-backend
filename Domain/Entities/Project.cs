using Domain.Entities.Base;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Project : BaseAuditableEntity, IActivityAuditingEntity
{
    private Project()
    {
    }

    private Project(string name, string projectManager, string clientEmail, string slackLink, DateTime startDate,
        DateTime endDate, DateTime? actualEndDate, decimal? completion)
    {
        Name = name;
        ProjectManagerId = projectManager;
        ClientEmail = clientEmail;
        SlackLink = slackLink;
        StartDate = startDate;
        EstimatedEndDate = endDate;
        ActualEndDate = actualEndDate;
        Completion = completion;

    }

    public void Update(string name, string projectManager, string clientEmail, string slackLink, DateTime startDate,
        DateTime endDate, DateTime? actualEndDate, decimal? completion)
    {
        Name = name;
        ProjectManagerId = projectManager;
        ClientEmail = clientEmail;
        SlackLink = slackLink;
        StartDate = startDate;
        EstimatedEndDate = endDate;
        ActualEndDate = actualEndDate;
        Completion = completion;

    }

    public static Project Create(string name, string projectManager, string clientEmail, string slackLink, DateTime startDate,
        DateTime endDate, DateTime? actualEndDate, decimal? completion)
    {
        var project = new Project(name,projectManager, clientEmail,slackLink,startDate,
        endDate, actualEndDate, completion);


        return project;
    }



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

    public string GetLogIdentifier()
    {
        return Name;
    }
}