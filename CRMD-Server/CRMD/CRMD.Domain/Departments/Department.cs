using CRMD.Domain.Attributes;

namespace CRMD.Domain.Departments
{
    public class Department
    {
        [IgnoreOn(enOperationMode.Add)]
        int DepartmentId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        string Name { get; set; } = string.Empty;
        short employeesCount { get; set; }
    }
}