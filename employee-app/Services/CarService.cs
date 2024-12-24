using System.Net.Http.Json;
using employeeapp.Dtos;

namespace employeeapp.Services;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/employee/cars";

    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CarDto>> GetAllCars()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<CarDto>>(BaseUrl);
        return response ?? Enumerable.Empty<CarDto>();
    }

    public async Task<CarDto> GetCar(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<CarDto>($"{BaseUrl}/{id}");
        return response ?? throw new Exception("Car not found");
    }

    public async Task<CarDto> AddCar(CarCreateDto car)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, car);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarDto>() 
               ?? throw new Exception("Failed to create car");
    }

    public async Task<CarDto> UpdateCar(int id, CarUpdateDto car)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", car);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CarDto>() 
               ?? throw new Exception("Failed to update car");
    }

    public async Task DeleteCar(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<CarDto>> GetNextCars(int lastId)
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<CarDto>>($"{BaseUrl}/next/{lastId}");
        return response ?? Enumerable.Empty<CarDto>();
    }
    
    public async Task<IEnumerable<CarDto>> GetPrevCars(int firstId)
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<CarDto>>($"{BaseUrl}/previous/{firstId}");
        return response ?? Enumerable.Empty<CarDto>();
    }
    
    public async Task<int> GetCarsCount()
    {
        var response = await _httpClient.GetFromJsonAsync<int>($"{BaseUrl}/count");
        return response;
    }
}