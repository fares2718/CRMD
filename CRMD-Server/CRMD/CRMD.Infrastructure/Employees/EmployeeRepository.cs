using CRMD.Domain.Employees;
using CRMD.Infrastructure.Generics;

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
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("restocafe.addemployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, employee.Name);
                    cmd.Parameters.AddWithValue("roles", NpgsqlDbType.Array | NpgsqlDbType.Smallint, employee.Roles);
                    cmd.Parameters.AddWithValue("phones", NpgsqlDbType.Array | NpgsqlDbType.Varchar, employee.Phones);
                    cmd.Parameters.AddWithValue("departmentid", employee.DepartmentId);
                    cmd.Parameters.AddWithValue("salary", NpgsqlDbType.Money, employee.Salary);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }*/
            await GenericRepository<Employee>.AddAsync(employee, _connectionString, "restocafe.addemployee");
        }

        public async Task DeleteEmployee(int id)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("restocafe.deleteemployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }*/
            await GenericRepository<Employee>.DeleteAsync(id, _connectionString, "restocafe.deleteemployee");
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = new List<EmployeeDto>();
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {

                using (var cmd = new NpgsqlCommand("SELECT * FROM restocafe.getallemployees()", conn))
                {
                    await conn.OpenAsync();
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
            return employees;*/
            using (var reader = await GenericRepository<List<EmployeeDto>>.
                GetAllAsync(_connectionString, "restocafe.getallemployees()"))
            {
                while (await reader.ReadAsync())
                {
                    var employee = Mapper.Map<EmployeeDto>(reader);
                    employees.Add(employee);
                }
                NpgsqlConnection.ClearAllPools();
                return employees;
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int Id)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("select * from restocafe.getemployeebyid(@id)", conn))
                {
                    cmd.Parameters.AddWithValue("id", Id);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var employee = Mapper.Map<EmployeeDto>(reader);
                            return employee;
                        }
                        return new EmployeeDto();
                    }
                }
            }*/
            using (var reader = await GenericRepository<EmployeeDto>
            .GetByIdAsync(Id, _connectionString, "restocafe.getemployeebyid(@id)"))
            {
                EmployeeDto employee = new EmployeeDto();
                if (reader != null && await reader.ReadAsync())
                {
                    employee = Mapper.Map<EmployeeDto>(reader);
                }
                NpgsqlConnection.ClearAllPools();
                return employee;
            }
        }

        public async Task UpdateEmployeeAsync(Employee newEmployeeDtat)
        {
            await GenericRepository<Employee>.UpdateAsync(newEmployeeDtat, _connectionString, "restocafe.updateemployee");
        }
    }
}