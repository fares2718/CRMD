namespace CRMD.Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public short[] Roles { get; set; } = null!;
        public string Department { get; set; } = null!;
        public decimal Salary { get; set; }
        public string[] Phones { get; set; } = null!;
    }
}