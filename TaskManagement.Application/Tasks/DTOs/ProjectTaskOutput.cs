using TaskManagement.Application.Authentication.DTOs;

namespace TaskManagement.Application.Tasks.DTOs
{
    public class ProjectTaskGetDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long UserId { get; set; }
        public UesrGetDto User { get; set; }
    }
}
