using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class EmployeeRepo : IEmployeeRepo
{
    public async Task<bool> ActivateEmployeeAsync(string employeeId)
    {
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                using (var cmd = new SqlCommand("SP_ActivateEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    await conn.OpenAsync();
                    var rowsAffected = await cmd.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
    }

    public async Task<string> AddEmployeeAsync(clsEmployee employee)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddEmployee", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserName", employee.UserName);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Role", (int)employee.Role);
                cmd.Parameters.AddWithValue("@Password", employee.Password);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                cmd.Parameters.AddWithValue("@HieredAt", employee.HieredAt);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                var outputIdParam = new SqlParameter("@NewEmployeeID", SqlDbType.NVarChar, 50)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return outputIdParam.Value.ToString()!;
            }
        }
    }

    public async Task<bool> DeactivateEmployeeAsync(string employeeId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_DeactivateEmployee", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> UpdateEmployeeSalaryAsync(decimal newSalary, string employeeId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdateEmployeeSalary", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@Salary", newSalary);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

}
