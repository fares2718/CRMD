using CRMD.Domain.Attributes;

namespace CRMD.Domain.Employees
{
    public class Employee
    {
        [IgnoreOn(enOperationMode.Add)]
        public int Id { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public string Name { get; set; } = null!;
        public short[] Roles { get; set; } = null!;
        public int DepartmentId { get; set; }
        public decimal Salary { get; set; }
        public string[] Phones { get; set; } = null!;
    }
}