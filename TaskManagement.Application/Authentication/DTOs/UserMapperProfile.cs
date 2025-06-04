using AutoMapper;
using TaskManagement.Domain.Users.Models;

namespace TaskManagement.Application.Authentication.DTOs
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UesrGetDto>();
        }
    }
}
