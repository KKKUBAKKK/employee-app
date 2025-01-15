using employeeapp.Helpers;

namespace employee_app.Dtos;

public class RentalHistoryDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Condition { get; set; }
    public decimal TotalPrice { get; set; }
    public RentalStatus Status { get; set; }
    public string RenterName { get; set; }
}