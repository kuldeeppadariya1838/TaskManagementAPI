using System.Security.Claims;
using System.Text;
using TaskManagementAPI.DTOs.Auth;
using TaskManagementAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TaskManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<TMUsers> Register(RegisterDto registerDto)
        {
            var user = new TMUsers
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password,
            };

            _context.TMUsers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _context.TMUsers.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
            {
                return null;
            }

            return CreateToken(user);
        }

        private string CreateToken(TMUsers user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
