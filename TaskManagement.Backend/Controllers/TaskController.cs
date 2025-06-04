using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Application.Tasks;
using TaskManagement.Application.Tasks.DTOs;

namespace TaskManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IProjectTaskService _taskService;

        public TaskController(IProjectTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectTaskGetDto>>> GetByLoggedInUser()
        {
            var userId = GetUserId();
            return await _taskService.GetByUserIdAsync(userId);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTaskGetDto>> Create([FromBody] CreateProjectTaskDto input)
        {
            var userId = GetUserId();
            return await _taskService.CreateAsync(input, userId);   
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectTaskGetDto>> Update(long id, [FromBody] UpdateProjectTaskDto input)
        {
            var userId = GetUserId();
            return await _taskService.UpdateAsync(input, id, userId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            var userId = GetUserId();
            return await _taskService.DeleteAsync(id, userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskGetDto>> Get(long id)
        {
            return await _taskService.GetByIdAsync(id);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProjectTaskGetDto>>> GetAll()
        {
            return await _taskService.GetAll();
        }

        [HttpPut("Complete/{id}")]
        public async Task<ActionResult<ProjectTaskGetDto>> Complete(long id)
        {
            var userId = GetUserId();
            return await _taskService.CompleteAsync(id, userId);
        }


        #region Private Methodes
        private long GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return long.TryParse(claim, out var id) ? id : throw new UnauthorizedAccessException("Invalid user ID");
        }
        #endregion
    }
}
