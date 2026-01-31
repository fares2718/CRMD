using CRMD.Domain.Departments;
using CRMD.Infrastructure.Generics;
using ErrorOr;

namespace CRMD.Infrastructure.Departments.Persistence
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddNewDepartmentAsync(Department department)
        {
            await GenericRepository<Department>.AddAsync(department, _connectionString, "restocafe.adddepartment");
        }

        public async Task DeleteDepartmentAsync(int Id)
        {
            await GenericRepository<Department>.DeleteAsync(Id, _connectionString, "restocafe.deletedepartment");
        }

        public async Task<List<Department>?> GetAllDepartmentsAsync()
        {
            var departments = new List<Department>();
            using (var reader = await GenericRepository<Department>
            .GetAllAsync(_connectionString, "restocafe.getdepartments()"))
            {
                if (reader == null || !reader.HasRows)
                    return null;
                while (await reader.ReadAsync())
                {
                    var department = Mapper.Map<Department>(reader);
                    departments.Add(department);
                }
                NpgsqlConnection.ClearAllPools();
                return departments;
            }
        }

        public async Task<Department?> GetDepartmentByIdAsync(int Id)
        {
            using (var reader = await GenericRepository<Department>
            .GetByIdAsync(Id, _connectionString, "restocafe.getdepartmentbyid(@id)"))
            {
                Department department = new Department();
                if (reader == null || !reader.HasRows)
                    return null;

                department = Mapper.Map<Department>(reader);
                NpgsqlConnection.ClearAllPools();
                return department;
            }
        }

        public async Task UpdateDepartmentAsync(Department newDepartmentData)
        {
            await GenericRepository<Department>.UpdateAsync(newDepartmentData, _connectionString, "restocafe.updatedepartment");
        }

    }
}