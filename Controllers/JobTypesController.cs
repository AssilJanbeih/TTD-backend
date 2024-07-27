using Microsoft.AspNetCore.Mvc;
using TTTD_Context.Services;

namespace TTD_Backend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class JobTypesController : ControllerBase
{
    private readonly JobTypeService _jobTypeService;

    public JobTypesController(JobTypeService jobTypeService)
    {
        _jobTypeService = jobTypeService;
    }

    // Get all job types
    [HttpGet]
    public async Task<ActionResult> GetJobTypes()
    {
        var jobTypes = await _jobTypeService.GetAllJobTypesAsync();
        return Ok(jobTypes);
    }

}
