namespace employee;

public class ReturnRecordDto
{
    public int EmployeeID { get; set; }
    public int RentalId { get; set; }
    public string Condition { get; set; }
    public string FrontPhotoUrl { get; set; }
    public string BackPhotoUrl { get; set; }

    public string RightPhotoUrl { get; set; }

    public string LeftPhotoUrl { get; set; }

    public string EmployeeNotes { get; set; }
    public DateTime ReturnDate { get; set; }
}
