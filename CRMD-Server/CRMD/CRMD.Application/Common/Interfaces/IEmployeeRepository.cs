namespace CRMD.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task AddNewEmployeeAsync(Employee employee);
        Task DeleteEmployee(int id);
        public Task<List<EmployeeDto>> GetAllEmployeesAsync();
        public Task<EmployeeDto> GetEmployeeByIdAsync(int Id);
        public Task UpdateEmployeeAsync(Employee newEmployeeData);
    }
}