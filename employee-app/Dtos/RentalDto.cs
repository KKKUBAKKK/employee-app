using employeeapp.Helpers;

namespace employeeapp.Dtos;

public class RentalDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public RentalStatus Status { get; set; }
    public String StartLocation { get; set; }
    public String EndLocation { get; set; }
}