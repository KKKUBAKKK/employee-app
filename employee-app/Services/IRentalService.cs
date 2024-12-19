using employee;
using employeeapp.Dtos;

namespace employeeapp.Services;

public interface IRentalService
{
    Task<IEnumerable<RentalDto>> GetAllRentals(RentalFilterDto? filter = null);
    Task<IEnumerable<RentalDto>> GetPendingReturns();
    Task<ReturnRecordDto> CompleteReturn(ReturnRecordDto returnRecord);
}