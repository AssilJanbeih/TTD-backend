using System.Net;
using Microsoft.AspNetCore.Mvc;
using Application.Project.Queries;
using Application.Project.Commands;
using Application.Projects.Commands;
using Presentation.Controllers;

namespace TTD_backend.Controllers.Projects;

public class ProjectsController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(AddProjectResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddProjectAsync([FromBody] AddProjectRequest request)
    {
        AddProjectCommand command = new(request);
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProjectAsync([FromBody] UpdateProjectRequest request)
    {
        UpdateProjectCommand command = new(request);
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<GetProjectsResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProjectsAsync(
        CancellationToken cancellationToken)
    {
        GetProjectsQuery query = new GetProjectsQuery();
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet]
    [Route("{projectId}")]
    [ProducesResponseType(typeof(GetProjectResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProjectAsync([FromRoute] int projectId)
    {
        GetProjectQuery query = new GetProjectQuery(projectId);
        return Ok(await Mediator.Send(query));
    }
}