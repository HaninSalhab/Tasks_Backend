using AutoMapper;
using TaskManagement.Domain.Tasks.Models;

namespace TaskManagement.Application.Tasks.DTOs
{
    public class ProjectTaskMapperProfile : Profile
    {
        public ProjectTaskMapperProfile()
        {
            CreateMap<ProjectTask, ProjectTaskGetDto>();
            CreateMap<CreateProjectTaskDto, ProjectTask>();
            CreateMap<UpdateProjectTaskDto, ProjectTask>();
        }
    }
}
