using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class WasteLogRepo : IWasteLogRepo
{
    public async Task<decimal> CalculateItemWasteAsync(int itemId, decimal LeftoverQuantity)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_CalculateItemWaste", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@LeftoverQuantity", LeftoverQuantity);
                var outputParam = new SqlParameter("@WasteQuantity", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.ReturnValue,
                    Precision = 18,
                    Scale = 2
                };
                cmd.Parameters.Add(outputParam);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return (decimal)outputParam.Value;
            }
        }
    }

    public async Task<List<clsWasteLog>> GetWasteLogsReportAsync(DateTime startDate, DateTime endDate)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetWasteLogsReport", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                var wasteLogs = new List<clsWasteLog>();
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var wasteLog = Mapper.MappWasteLog(reader);
                        wasteLogs.Add(wasteLog);
                    }
                }
                return wasteLogs;
            }
        }
    }

    public async Task<int> LogWasteAsync(int itemId, decimal quantity)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_LogWaste", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                var outputParam = new SqlParameter("@NewWasteLogId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputParam);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return (int)outputParam.Value;
            }
        }
    }
}
