using System.Net.Http.Headers;
using Blazored.LocalStorage;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using employee_app.Models;
using Microsoft.IdentityModel.JsonWebTokens;

namespace employee_app.Services;

public class EmployeeAuthenticationStateProvider : AuthenticationStateProvider
{
    public ILocalStorageService LocalStorageService { get; }
    public ILoginService LoginService { get; }
    private readonly HttpClient _httpClient;

    public EmployeeAuthenticationStateProvider(ILocalStorageService localStorageService, ILoginService loginService, HttpClient httpClient)
    {
        LocalStorageService = localStorageService;
        LoginService = loginService;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await LocalStorageService.GetItemAsync<string>("authToken");
        var username = await LocalStorageService.GetItemAsync<string>("username");
        var employeeId = await LocalStorageService.GetItemAsync<string>("employeeId");

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(employeeId))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var identity = new ClaimsIdentity(new[]
        {
            new Claim("Token", token),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, employeeId)
        }, "apiauth_type");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(Employee employee)
    {
        await LocalStorageService.SetItemAsync("authToken", employee.Token);
        await LocalStorageService.SetItemAsync("username", employee.Username);
        await LocalStorageService.SetItemAsync("employeeId", employee.Id.ToString());
        
        var identity = new ClaimsIdentity(new[]
        {
            new Claim("Token", employee.Token),
            new Claim(ClaimTypes.Name, employee.Username),
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString())
        }, "apiauth_type");
        var user = new ClaimsPrincipal(identity);
        var authState = Task.FromResult(new AuthenticationState(user));

        NotifyAuthenticationStateChanged(authState);
    }

    public async Task MarkUserAsLoggedOut()
    {
        await LocalStorageService.RemoveItemAsync("authToken");
        await LocalStorageService.RemoveItemAsync("username");
        await LocalStorageService.RemoveItemAsync("employeeId");

        _httpClient.DefaultRequestHeaders.Authorization = null;

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
}