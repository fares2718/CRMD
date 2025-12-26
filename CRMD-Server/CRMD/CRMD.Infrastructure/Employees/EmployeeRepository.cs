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
                using (var cmd = new NpgsqlCommand("addnewemployee", conn))
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
    }
}