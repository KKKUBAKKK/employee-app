using employeeapp.Helpers;

namespace employeeapp.Dtos;

public class CarCreateDto
{
    public string Producer { get; set; }
    
    public string Model { get; set; }
    
    public int YearOfProduction { get; set; }
    
    public int NumberOfSeats { get; set; }
    
    public CarType Type { get; set; }
    
    public string Location { get; set; } = "Plac Politechniki, Warszawa";
}