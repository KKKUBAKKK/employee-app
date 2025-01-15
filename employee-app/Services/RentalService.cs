using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using employee_app.Dtos;
using employee;
using employeeapp.Dtos;

namespace employeeapp.Services;

public class RentalService : IRentalService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/employee/rentals";

    public RentalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RentalDto>> GetAllRentals(RentalFilterDto? filter = null)
    {
        var queryString = "";
        if (filter?.Status != null)
        {
            queryString = $"?Status={filter.Status}";
        }

        var response = await _httpClient.GetFromJsonAsync<IEnumerable<RentalDto>>($"{BaseUrl}{queryString}");
        return response ?? Enumerable.Empty<RentalDto>();
    }

    public async Task<IEnumerable<RentalDto>> GetPendingReturns()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<RentalDto>>($"{BaseUrl}/pending-returns");
        return response ?? Enumerable.Empty<RentalDto>();
    }

    public async Task<ReturnRecordDto> CompleteReturn(ReturnRecordDto returnRecord)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(returnRecord.EmployeeID.ToString()), "EmployeeID");
        content.Add(new StringContent(returnRecord.RentalId.ToString()), "RentalId");
        content.Add(new StringContent(returnRecord.Condition), "Condition");
        content.Add(new StringContent(returnRecord.FrontPhotoUrl), "FrontPhotoUrl");
        content.Add(new StringContent(returnRecord.BackPhotoUrl), "BackPhotoUrl");
        content.Add(new StringContent(returnRecord.RightPhotoUrl), "RightPhotoUrl");
        content.Add(new StringContent(returnRecord.LeftPhotoUrl), "LeftPhotoUrl");
        content.Add(new StringContent(returnRecord.EmployeeNotes), "EmployeeNotes");
        content.Add(new StringContent(returnRecord.ReturnDate.ToString("yyyy-MM-ddTHH:mm:ss")), "ReturnDate");
        
        // var content = new StringContent(JsonSerializer.Serialize(returnRecord), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{BaseUrl}/complete-return", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode 
            && !response.StatusCode.Equals(HttpStatusCode.NotFound) 
            && !responseContent.Equals("Customer API not found"))
        {
            response.EnsureSuccessStatusCode();
        }

        return await response.Content.ReadFromJsonAsync<ReturnRecordDto>() 
               ?? throw new Exception("Failed to complete return");
    }
    
    public async Task<List<RentalHistoryDto>> GetVehicleRentalHistory(int vehicleId)
    {
        // Get all rentals for the vehicle
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<RentalHistoryDto>>($"{BaseUrl}/history/{vehicleId}");
        return response?.ToList() ?? new List<RentalHistoryDto>();
    }
}