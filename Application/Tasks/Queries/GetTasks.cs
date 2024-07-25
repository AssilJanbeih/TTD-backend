using Application.Abstraction.Messaging;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Utils.CurrentUser;

namespace Application.Task.Queries;

public sealed record GetTasksQuery(
    int ProjectId
) : IQuery<IReadOnlyList<GetTasksResponse>>;

public sealed record GetTasksResponse(
    int Id,
    string Name,
    string ProjectId,
    string Assignee,
    string Status,
    DateTime StartDate,
    DateTime EstimatedEndDate,
    DateTime? ActualEndDate
);

public class GetTasksHandler : IQueryHandler<GetTasksQuery, IReadOnlyList<GetTasksResponse>>
{
    private readonly TTDContext _context;
    private readonly UserContextBaseService _userContextBaseService;

    public GetTasksHandler(TTDContext context, UserContextBaseService userContextBaseService)
    {
        _context = context;
        _userContextBaseService = userContextBaseService;
    }

    public async Task<IReadOnlyList<GetTasksResponse>> Handle(GetTasksQuery query,
        CancellationToken cancellationToken)
    {
        return await _context
            .Tasks
            .Include(p => p.Project)
            .Include(p => p.Assignee)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new GetTasksResponse(
                p.Id,
                p.Name,
                p.Project.Name,
                p.Assignee.Name,
                p.Status,
                p.StartDate,
                p.EstimatedEndDate,
                p.ActualEndDate
            ))
            .ToListAsync(cancellationToken);
    }
}