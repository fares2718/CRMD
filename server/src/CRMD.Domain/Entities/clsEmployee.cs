using System;

namespace CRMD.Domain.Entities;

public class clsEmployee
{
    public enum enRole
    {
        Witer = 1,
        Bartender = 2,
        Accountan = 4,
        Manager = 8
    }
    public string EmployeeID { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public enRole Role { get; set; }
    public string Password { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime HieredAt { get; set; }
    public decimal Salary { get; set; }
}
