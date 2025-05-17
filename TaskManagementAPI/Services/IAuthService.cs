using TaskManagementAPI.DTOs.Auth;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IAuthService
    {
        Task<TMUsers> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);

    }
}
