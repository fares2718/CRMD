using CRMD.Domain.Departments;

namespace CRMD.Application.Common.Interfaces
{
    public interface IDepartmentRepository
    {
        Task AddNewDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int Id);
        Task<List<Department>?> GetAllDepartmentsAsync();
        Task<Department?> GetDepartmentByIdAsync(int Id);
        Task UpdateDepartmentAsync(Department newDepartmentData);
    }
}