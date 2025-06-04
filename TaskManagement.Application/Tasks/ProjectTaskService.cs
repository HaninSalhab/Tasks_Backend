using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Tasks.DTOs;
using TaskManagement.Data;
using TaskManagement.Domain.Tasks;
using TaskManagement.Domain.Tasks.Models;

namespace TaskManagement.Application.Tasks
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly TaskManagementDbContext _context;
        private readonly IMapper _mapper;

        public ProjectTaskService(TaskManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectTaskGetDto> CompleteAsync(long taskId, long userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
                throw new Exception("Task not found.");

            task.Complete(userId);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectTaskGetDto>(task);
        }

        public async Task<ProjectTaskGetDto> CreateAsync(CreateProjectTaskDto input, long userId)
        {
            var task = _mapper.Map<ProjectTask>(input);
            task.CreatedAt = DateTime.UtcNow;
            task.UserId = userId;
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectTaskGetDto>(task);
        }

        public async Task<bool> DeleteAsync(long taskId, long userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
           
            if (task == null)
                return false;
           
            if (userId != task.UserId)
                throw new Exception("This is not your task to delete it.");
           
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProjectTaskGetDto>> GetAll()
        {
            var tasks = await _context.Tasks.Include(x => x.User).ToListAsync();
            return _mapper.Map<List<ProjectTaskGetDto>>(tasks);
        }

        public async Task<ProjectTaskGetDto> GetByIdAsync(long taskId)
        {
            var task = await _context.Tasks.Include(x => x.User).FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null)
                throw new Exception("Task not found.");

            return _mapper.Map<ProjectTaskGetDto>(task);
        }

        public async Task<List<ProjectTaskGetDto>> GetByUserIdAsync(long userId)
        {
            var tasks = await _context.Tasks.Include(x => x.User)
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<ProjectTaskGetDto>>(tasks);
        }

        public async Task<ProjectTaskGetDto> UpdateAsync(UpdateProjectTaskDto input, long taskId, long userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
           
            if (task == null)
                throw new Exception("Task not found.");
         
            if (userId != task.UserId)
                throw new Exception("This is not your task to edit it.");

            task.UserId = userId;
            _mapper.Map(input, task);
            task.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectTaskGetDto>(task);
        }
    }
}
