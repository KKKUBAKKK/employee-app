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

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    public async Task MarkUserAsAuthenticated(Employee employee)
    {
        await LocalStorageService.SetItemAsync("authToken", employee.Token);
        await LocalStorageService.SetItemAsync("username", employee.Username);
        await LocalStorageService.SetItemAsync("employeeId", employee.Id.ToString());
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, employee.Username) }, "jwt");
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

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JsonWebTokenHandler();
        var token = handler.ReadJsonWebToken(jwt);
        return token.Claims;
    }
}