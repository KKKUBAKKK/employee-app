namespace employee;

public class ReturnRecordDto
{
    public int RentalId { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string? EmployeeNotes { get; set; }
}
