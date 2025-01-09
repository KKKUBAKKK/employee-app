namespace employee_app.Models;

public class Employee
{
    public Employee(int id, string token, string username)
    {
        Id = id;
        Token = token;
        Username = username;
    }
    
    public int Id { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
}