using System.Net;
using Application.JobTypes.Queries.GetJobTypesDropdown;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class JobTypesController : BaseController
{
    [HttpGet]
    [Route("dropdown")]
    [ProducesResponseType(typeof(IReadOnlyList<GetJobTypesDropdownResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetJobTypesDropdownAsync()
    {
        GetJobTypesDropdownQuery query = new();
        return Ok(await Mediator.Send(query));
    }
}