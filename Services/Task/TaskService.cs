using Microsoft.EntityFrameworkCore;
using TTD_Backend.DTOs;

namespace TTD_Backend.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly TTTDContext _context;

        public TaskService(TTTDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(int projectId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Where(t => t.ProjectId.Equals(projectId))
                .Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Priority = t.Priority,
                    Status = t.Status,
                    StartDate = t.StartDate,
                    EstimatedEndDate = t.EstimatedEndDate,
                    ActualEndDate = t.ActualEndDate,
                    ProjectName = t.Project.Name,
                    AssigneeName = t.Assignee.Name
                })
                .ToListAsync();
        }

        public async Task<TaskResponseDto> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Assignee)
                .Where(t => t.Id == id)
                .Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Priority = t.Priority,
                    Status = t.Status,
                    StartDate = t.StartDate,
                    EstimatedEndDate = t.EstimatedEndDate,
                    ActualEndDate = t.ActualEndDate,
                    ProjectName = t.Project.Name,
                    AssigneeName = t.Assignee.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Models.Task> CreateTaskAsync(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            var project = await _context.Projects
                .Where(p => p.Id.Equals(task.ProjectId))
                .FirstOrDefaultAsync();
            project.UpdateCompletion();

            return task;
        }

        public async Task<Models.Task> UpdateTaskAsync(Models.Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var project = await _context.Projects
                .Where(p => p.Id.Equals(task.ProjectId))
                .FirstOrDefaultAsync();
            project.UpdateCompletion();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            var project = await _context.Projects
                .Where(p => p.Id.Equals(task.ProjectId))
                .FirstOrDefaultAsync();
            project.UpdateCompletion();
            return true;
        }
    }
}
