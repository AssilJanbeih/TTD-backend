using Microsoft.EntityFrameworkCore;
using TTD_Backend.DTOs;

namespace TTD_Backend.Services.Project
{
    public class ProjectService : IProjectService
    {
        private readonly TTTDContext _context;

        public ProjectService(TTTDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Select(p => new ProjectResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ClientEmail = p.ClientEmail,
                    SlackLink = p.SlackLink,
                    StartDate = p.StartDate,
                    EstimatedEndDate = p.EstimatedEndDate,
                    ActualEndDate = p.ActualEndDate,
                    Completion = p.Completion,
                    ProjectManagerName = p.ProjectManager.Name, 
                    Tasks = p.Tasks.Select(t => new TaskResponseDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Priority = t.Priority,
                        Status = t.Status,
                        StartDate = t.StartDate,
                        EstimatedEndDate = t.EstimatedEndDate,
                        ActualEndDate = t.ActualEndDate,
                        ProjectName = p.Name, 
                        AssigneeName = t.Assignee.Name 
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<ProjectResponseDto> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.Id == id)
                .Select(p => new ProjectResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ClientEmail = p.ClientEmail,
                    SlackLink = p.SlackLink,
                    StartDate = p.StartDate,
                    EstimatedEndDate = p.EstimatedEndDate,
                    ActualEndDate = p.ActualEndDate,
                    Completion = p.Completion,
                    ProjectManagerName = p.ProjectManager.Name, 
                    Tasks = p.Tasks.Select(t => new TaskResponseDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Priority = t.Priority,
                        Status = t.Status,
                        StartDate = t.StartDate,
                        EstimatedEndDate = t.EstimatedEndDate,
                        ActualEndDate = t.ActualEndDate,
                        ProjectName = p.Name, 
                        AssigneeName = t.Assignee.Name 
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<Models.Project> CreateProjectAsync(Models.Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Models.Project> UpdateProjectAsync(Models.Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
