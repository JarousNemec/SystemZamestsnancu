namespace SystemZamestsnancu.Data;

public class Employee
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public virtual Address Address { get; set; }
    public DateTime Birthdate{ get; set; }
}