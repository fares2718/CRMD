using System;

namespace CRMD.Domain.Entities;

public class clsEmployee
{
    public enum enRole
    {
        Witer = 1,
        Accountant = 2,
        Manager = 3,
        Bartender = 4,
        Owner = 5
    }
    public string EmployeeID { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public enRole Role { get; set; }
    public string Password { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime HieredAt { get; set; }
}
