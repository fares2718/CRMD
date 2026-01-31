using CRMD.Domain.Attributes;

namespace CRMD.Domain.Departments
{
    public class Department
    {
        [IgnoreOn(enOperationMode.Add)]
        public int DepartmentId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public string Name { get; set; } = string.Empty;
        public short employeesCount { get; set; }
    }
}