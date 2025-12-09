using System;
using System.Data;
using CRMD.Domain.Repos.Interfaces;
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
