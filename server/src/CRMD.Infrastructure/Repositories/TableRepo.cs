using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class TableRepo : ITableRepo
{
    public async Task<int> AddTableAsync(clsTable table)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using ( var cmd = new SqlCommand("SP_AddTable", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", table.Name);
                cmd.Parameters.AddWithValue("@WaiterId", table.WaiterId);
                cmd.Parameters.AddWithValue("@Status", table.Status);
                var outputIdParam = new SqlParameter("@NewTableId", System.Data.SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return Convert.ToInt32(outputIdParam.Value);
            }
        }
    }
}
