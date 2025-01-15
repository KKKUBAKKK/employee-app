using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using employee_app;
using employee_app.Services;
using employeeapp.Services;

using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://car-rental-api-chezbchwebfggwcd.canadacentral-01.azurewebsites.net") });
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5001") });

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IRentalService, RentalService>();

// Local storage
builder.Services.AddBlazoredLocalStorage();

// Login service
builder.Services.AddScoped<ILoginService, LoginService>();

// Authentication
builder.Services.AddScoped<EmployeeAuthenticationStateProvider, EmployeeAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, EmployeeAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();