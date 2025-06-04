using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Authentication;
using TaskManagement.Application.Authentication.DTOs;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthDto>> Register([FromBody] RegisterDto input)
        {
            return Ok(await _authService.Register(input));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginDto input)
        {
            return Ok(await _authService.Login(input));
        }
    }
}
