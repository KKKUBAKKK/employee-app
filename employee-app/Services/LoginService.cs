using System.Net.Http.Json;
using employee_app.Dtos;
using employee_app.Models;

namespace employee_app.Services;

public class LoginService : ILoginService
{
    public HttpClient HttpClient { get; }
    
    public LoginService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
    
    public async Task<Employee> Login(LoginDto loginInfo)
    {
        var response = await HttpClient.PostAsJsonAsync("api/auth/login", loginInfo);

        if (!response.IsSuccessStatusCode)
        {
            return new Employee(-1, "", "");
        }
        
        var acceptLogin = await response.Content.ReadFromJsonAsync<AcceptLoginDto>();
        if (acceptLogin == null)
        {
            return new Employee(-1, "", "");
        }
        
        return new Employee(acceptLogin.employeeId, acceptLogin.token, loginInfo.Username);
    }
}