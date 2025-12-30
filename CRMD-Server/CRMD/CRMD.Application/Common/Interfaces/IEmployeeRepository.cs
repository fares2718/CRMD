namespace CRMD.Application.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task AddNewEmployeeAsync(Employee employee);
        public Task<List<EmployeeDto>> GetAllEmployeesAsync();
    }
}