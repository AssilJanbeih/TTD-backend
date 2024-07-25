using Application.Abstraction.Messaging;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Utils.CurrentUser;

namespace Application.Project.Queries;

public sealed record GetProjectsQuery(
) : IQuery<IReadOnlyList<GetProjectsResponse>>;

public sealed record GetProjectsResponse(
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

public class GetProjectsHandler : IQueryHandler<GetProjectsQuery, IReadOnlyList<GetProjectsResponse>>
{
    private readonly TTDContext _context;
    private readonly UserContextBaseService _userContextBaseService;

    public GetProjectsHandler(TTDContext context, UserContextBaseService userContextBaseService)
    {
        _context = context;
        _userContextBaseService = userContextBaseService;
    }

    public async Task<IReadOnlyList<GetProjectsResponse>> Handle(GetProjectsQuery query,
        CancellationToken cancellationToken)
    {
        return await _context
            .Projects
            .Include(p => p.ProjectManager)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new GetProjectsResponse(
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
            .ToListAsync(cancellationToken);
    }
}