using TTD_Backend.DTOs;

namespace TTD_Backend.Services.Project
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync();
        Task<ProjectResponseDto> GetProjectByIdAsync(int id);
        Task<Models.Project> CreateProjectAsync(Models.Project project);
        Task<Models.Project> UpdateProjectAsync(Models.Project project);
        Task<bool> DeleteProjectAsync(int id);
    }
}
