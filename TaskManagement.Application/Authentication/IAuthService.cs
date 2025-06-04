using TaskManagement.Application.Authentication.DTOs;

namespace TaskManagement.Application.Authentication
{
    public interface IAuthService
    {
        Task<AuthDto> Register(RegisterDto request);
        Task<AuthDto> Login(LoginDto request);
    }
}
