using employeeapp.Helpers;

namespace employeeapp.Dtos;

public class CarUpdateDto
{
    public string Location { get; set; } = "Plac Politechniki, Warszawa";
    public bool IsAvailable { get; set; } = true;
    public CarType Type { get; set; } = CarType.economy;
}