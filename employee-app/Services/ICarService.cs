using employeeapp.Dtos;

namespace employeeapp.Services;

public interface ICarService
{
    Task<IEnumerable<CarDto>> GetAllCars();
    Task<CarDto> GetCar(int id);
    Task<CarDto> AddCar(CarCreateDto car);
    Task<CarDto> UpdateCar(int id, CarUpdateDto car);
    Task DeleteCar(int id);
}