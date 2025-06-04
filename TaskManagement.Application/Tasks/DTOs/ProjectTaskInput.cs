namespace TaskManagement.Application.Tasks.DTOs
{
    public class CreateProjectTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateProjectTaskDto : CreateProjectTaskDto
    {
        public bool Completed { get; set; }
    }
}
