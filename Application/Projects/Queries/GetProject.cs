using Application.Abstraction.Messaging;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Project.Queries;

public sealed record GetProjectQuery(
    int ProjectId
) : IQuery<GetProjectResponse>;

public sealed record GetProjectResponse(
     int Id,
    string Name,
    string ProjectManager,
    string ClientEmail,
    string SlackLink,
    DateTime StartDate,
    DateTime EstimatedEndDate,
    DateTime? ActualEndDate,
    Decimal? Completion
);

public class GetProjectHandler : IQueryHandler<GetProjectQuery, GetProjectResponse>
{
    private readonly TTDContext _context;

    public GetProjectHandler(TTDContext context)
    {
        _context = context;
    }

    public async Task<GetProjectResponse> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var task = await _context
            .Projects
            .Include(e => e.ProjectManager)
            .Where(p => p.Id.Equals(request.ProjectId))
            .Select(p => new GetProjectResponse(
                p.Id,
                p.Name,
                p.ProjectManager.Name,
                p.ClientEmail,
                p.SlackLink,
                p.StartDate,
                p.EstimatedEndDate,
                p.ActualEndDate,
                p.Completion
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (task is null)
        {
            throw new TTDException("Entity not found");
        }

        return task;
    }
}