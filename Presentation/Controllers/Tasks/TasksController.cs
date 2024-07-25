using Microsoft.AspNetCore.Mvc;
using Application.Task.Commands;
using Application.Task.Queries;
using System.Net;

namespace Presentation.Controllers.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TasksController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(AddTaskResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddTasks([FromBody] AddTaskRequest request)
    {
        AddTaskCommand command = new(request);
        return StatusCode((int)HttpStatusCode.Created, await Mediator.Send(command));
    }

    [HttpPost]
    [Route("delete-bulk")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteTasks([FromBody] DeleteTasksRequest request)
    {
        DeleteTasksCommand command = new(request);
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateTasks([FromBody] UpdateTaskRequest request)
    {
        UpdateTaskCommand command = new(request);
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("project/{projectId}")]
    [ProducesResponseType(typeof(IReadOnlyList<GetTasksResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTask([FromRoute] int projectId)
    {
        GetTasksQuery query = new GetTasksQuery(projectId);
        return Ok(await Mediator.Send(query));
    }


    [HttpGet]
    [Route("{taskId}")]
    [ProducesResponseType(typeof(GetTaskResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTaskByTaskId([FromRoute] int taskId)
    {
        GetTaskQuery query = new GetTaskQuery(taskId);
        return Ok(await Mediator.Send(query));

    }
}