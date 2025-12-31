using CRMD.Domain.Employees;

namespace CRMD.Infrastructure.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddNewEmployeeAsync(Employee employee)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("restocafe.addnewemployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", employee.Name);
                    cmd.Parameters.AddWithValue("roles", employee.Roles);
                    cmd.Parameters.AddWithValue("phones", employee.Phones);
                    cmd.Parameters.AddWithValue("departmentid", employee.DepartmentId);
                    cmd.Parameters.AddWithValue("salary", employee.Salary);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = new List<EmployeeDto>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand("SELECT * FROM restocafe.getallemployees()", conn))
                {

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var employee = Mapper.Map<EmployeeDto>(reader);
                            employees.Add(employee);
                        }
                    }
                }
            }
            return employees;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int Id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("select * from restocafe.getemployeebyid", conn))
                {
                    cmd.Parameters.AddWithValue("id", Id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var employee = Mapper.Map<EmployeeDto>(reader);
                        return employee;
                    }
                }
            }
        }
    }
}