using employeeapp.Helpers;

namespace employeeapp.Dtos;

public class CarDto
{
    public int Id { get; set; }
    public string RentalService = Constants.RentalName;
    public string Producer { get; set; }
    public string Model { get; set; }
    public int YearOfProduction { get; set; }
    public int NumberOfSeats { get; set; }
    public CarType Type { get; set; }
    public bool IsAvailable { get; set; }
    public string Location { get; set; }
}