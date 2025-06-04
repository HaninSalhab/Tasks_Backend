using TaskManagement.Application.Tasks.DTOs;

namespace TaskManagement.Application.Tasks
{
    public interface IProjectTaskService
    {
        Task<ProjectTaskGetDto> CreateAsync(CreateProjectTaskDto input, long userId);
        Task<ProjectTaskGetDto> UpdateAsync(UpdateProjectTaskDto input, long taskId, long userId);
        Task<bool> DeleteAsync(long taskId, long userId);
        Task<ProjectTaskGetDto> GetByIdAsync(long taskId);
        Task<List<ProjectTaskGetDto>> GetAll();
        Task<ProjectTaskGetDto> CompleteAsync(long taskId, long userId);
        Task<List<ProjectTaskGetDto>> GetByUserIdAsync(long userId);
    }
}
