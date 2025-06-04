using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Authentication.DTOs;
using TaskManagement.Data;
using TaskManagement.Domain.Users.Models;

namespace TaskManagement.Application.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly TaskManagementDbContext _context; 
        private readonly IConfiguration _configuration;

        public AuthService(TaskManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthDto> Register(RegisterDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
                throw new Exception("Your email is already registered, please try to login.");

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                MobileNumber = request.MobileNumber,
                EmailConfirmed = false,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthDto
            {
                Token = token,
                Email = user.Email,
                UserName = user.UserName
            };
        }

        public async Task<AuthDto> Login(LoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                throw new Exception("User doesn't exist.");

            var verified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!verified)
                throw new Exception("Invalid email or password.");

            var token = GenerateJwtToken(user);

            return new AuthDto
            {
                Token = token,
                Email = user.Email,
                UserName = user.UserName
            };
        }

        #region Private Methods
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var expiryMinutes = jwtSettings.GetValue<int>("ExpiryMinutes");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(
               claims: new[]
               {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
               },
               expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
               signingCredentials: creds
           );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
