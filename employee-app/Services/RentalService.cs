using System.Net.Http.Json;
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
        content.Add(new StringContent(returnRecord.FrontPhotoUrl ?? ""), "FrontPhotoUrl");
        content.Add(new StringContent(returnRecord.BackPhotoUrl ?? ""), "BackPhotoUrl");
        content.Add(new StringContent(returnRecord.RightPhotoUrl ?? ""), "RightPhotoUrl");
        content.Add(new StringContent(returnRecord.LeftPhotoUrl ?? ""), "LeftPhotoUrl");
        content.Add(new StringContent(returnRecord.EmployeeNotes ?? ""), "EmployeeNotes");
        content.Add(new StringContent(returnRecord.ReturnDate.ToString("yyyy-MM-ddTHH:mm:ss")), "ReturnDate");

        var response = await _httpClient.PostAsync($"{BaseUrl}/complete-return", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ReturnRecordDto>() 
               ?? throw new Exception("Failed to complete return");
        
        // var content = new MultipartFormDataContent();
        // content.Add(new StringContent(returnRecord.EmployeeID.ToString()), "EmployeeID");
        // content.Add(new StringContent(returnRecord.RentalId.ToString()), "RentalId");
        // content.Add(new StringContent(returnRecord.Condition), "Condition");
        // content.Add(new StringContent(returnRecord.FrontPhotoUrl ?? ""), "FrontPhotoUrl");
        // content.Add(new StringContent(returnRecord.BackPhotoUrl ?? ""), "BackPhotoUrl");
        // content.Add(new StringContent(returnRecord.RightPhotoUrl ?? ""), "RightPhotoUrl");
        // content.Add(new StringContent(returnRecord.LeftPhotoUrl ?? ""), "LeftPhotoUrl");
        // content.Add(new StringContent(returnRecord.EmployeeNotes ?? ""), "EmployeeNotes");
        // content.Add(new StringContent(returnRecord.ReturnDate.ToString()), "ReturnDate");
        //
        // var response = await _httpClient.PostAsync($"{BaseUrl}/complete-return", content);
        // response.EnsureSuccessStatusCode();
        //
        // return await response.Content.ReadFromJsonAsync<ReturnRecordDto>() 
        //        ?? throw new Exception("Failed to complete return");
    }
}