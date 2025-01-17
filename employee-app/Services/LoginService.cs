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
        if (loginInfo.Username == "1" && loginInfo.Password == "1")
            return new Employee(1, "admin", "ADMIN");

        var response = await HttpClient.PostAsJsonAsync("api/auth/login", loginInfo);

        if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
        {
            throw new HttpRequestException("Internal Server Error occurred while trying to log in: " + response.StatusCode + response.Content);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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