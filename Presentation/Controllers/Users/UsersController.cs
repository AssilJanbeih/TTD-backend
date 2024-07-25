using System.Net;
using Application.Users.Commands.AddUser;
using Application.Users.Commands.DeleteUsers;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetUser;
using Application.Users.Queries.GetUsers;
using Application.Users.Queries.GetUsersDropdown;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Users;

public class UsersController : BaseController
{
    [HttpGet]
    [Route("{userId}")]
    [ProducesResponseType(typeof(GetUserResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserAsync([FromRoute] string userId)
    {
        var query = new GetUserQuery(new GetUserRequest
        {
            UserEntityId = userId
        });
        return Ok(await Mediator.Send(query));
    }
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<GetUsersResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUsersAsync()
    {
        var query = new GetUsersQuery();
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddUserResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddUserAsync(AddUserRequest request)
    {
        var command = new AddUserCommand(request);
        return StatusCode((int)HttpStatusCode.Created, await Mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(request);
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    [Route("delete-bulk")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteUsersAsync(DeleteUsersRequest request)
    {
        var command = new DeleteUsersCommand(request);
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("pms-dropdown")]
    [ProducesResponseType(typeof(IReadOnlyList<GetUsersDropdownResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPmsDropdownAsync([FromQuery] GetUsersDropdownRequest request)
    {
        GetUsersDropdownQuery query = new(
            new GetUsersDropdownRequest(JobTypeEnum.ProjectManager, request.DesignatedCountry, request.ByDepartment,
                request.department));
        return Ok(await Mediator.Send(query));
    }

}