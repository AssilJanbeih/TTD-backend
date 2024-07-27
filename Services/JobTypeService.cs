using Microsoft.EntityFrameworkCore;
using Models;
using TTD_Backend;

public class JobTypeService
{
    private readonly TTTDContext _context;

    public JobTypeService(TTTDContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobType>> GetAllJobTypesAsync()
    {
        return await _context.JobTypes.ToListAsync();
    }
}