using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;
using CRMD.Infrastructure.Mappers;

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

    public async Task<bool> AssignWaiterToTableAsync(int tableId, string waiterId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using ( var cmd = new SqlCommand("SP_AssignWaiterToTable", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TableId", tableId);
                cmd.Parameters.AddWithValue("@WaiterId", waiterId);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<List<clsTable>> GetAllTablesAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using ( var cmd = new SqlCommand("SP_GetAllTables", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var tables = new List<clsTable>();
                    while (await reader.ReadAsync())
                    {
                        var table = Mapper.MapTable(reader);
                        tables.Add(table);
                    }
                    return tables;
                }
            }
        }
    }

    public async Task<bool> MergeTablesAsync(int sourceTableId, int targetTableId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using ( var cmd = new SqlCommand("SP_MergeTables", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SourceTableId", sourceTableId);
                cmd.Parameters.AddWithValue("@TargetTableId", targetTableId);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> UpdateTableStatusAsync(int tableId, short status)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using ( var cmd = new SqlCommand("SP_UpdateTableStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TableId", tableId);
                cmd.Parameters.AddWithValue("@Status", status);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
