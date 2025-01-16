using employee_app.Dtos;
using employee;
using employeeapp.Dtos;

namespace employeeapp.Services;

public interface IRentalService
{
    Task<IEnumerable<RentalDetailsDto>> GetAllRentals(RentalFilterDto? filter = null);
    Task<IEnumerable<RentalDto>> GetPendingReturns();
    Task<ReturnRecordDto> CompleteReturn(ReturnRecordDto returnRecord);
    Task<List<RentalHistoryDto>> GetVehicleRentalHistory(int vehicleId);
}