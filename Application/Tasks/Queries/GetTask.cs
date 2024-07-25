using Application.Abstraction.Messaging;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Task.Queries;

public sealed record GetTaskQuery(
    int TaskId
) : IQuery<GetTaskResponse>;

public sealed record GetTaskResponse(
     int Id,
    string Name,
    string ProjectId,
    string Assignee,
    string Status,
    DateTime StartDate,
    DateTime EstimatedEndDate,
    DateTime? ActualEndDate
);

public class GetTaskHandler : IQueryHandler<GetTaskQuery, GetTaskResponse>
{
    private readonly TTDContext _context;

    public GetTaskHandler(TTDContext context)
    {
        _context = context;
    }

    public async Task<GetTaskResponse> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _context
            .Tasks
            .Include(e => e.Project)
            .Include(e => e.Assignee)
            .Where(p => p.Id.Equals(request.TaskId))
            .Select(p => new GetTaskResponse(
                p.Id,
                p.Name,
                p.ProjectId,
                p.AssigneeId,
                p.Status,
                p.StartDate,
                p.EstimatedEndDate,
                p.ActualEndDate
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (task is null)
        {
            throw new TTDException("Entity not found");
        }

        return task;
    }
}