namespace employeeapp.Dtos;

public class RentalDto
{
    public int Id { get; set; }
    public CarDto? Car { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    // Add other properties based on your backend DTO
}