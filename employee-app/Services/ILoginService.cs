using employee_app.Dtos;
using employee_app.Models;

namespace employee_app.Services;

public interface ILoginService
{
    public Task<Employee> Login(LoginDto loginInfo);
    // public Task<LoginDto> GetUserByAccessTokenAsync(string accessToken);
    // public Task<LoginDto> RefreshTokenAsync(RefreshRequest refreshRequest);
}