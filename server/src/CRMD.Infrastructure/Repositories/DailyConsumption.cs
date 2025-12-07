using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class DailyConsumption : IDailyConsumption
{
    public async Task<int> AddDailyConsumptionAsync(Queue<clsDailyConsumption> dailyConsumptions)
    {
        using ( var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            DataTable tvpConsumption = new DataTable();;
            tvpConsumption.Columns.Add("MenuItemId",typeof(int));
            tvpConsumption.Columns.Add("ConsumeDate",typeof(DateTime));
            tvpConsumption.Columns.Add("Quantity",typeof(decimal));
            tvpConsumption.Columns.Add("Notes",typeof(string));
            while (dailyConsumptions.Count > 0)
            {
                var consumption = dailyConsumptions.Dequeue();
                tvpConsumption.Rows.Add(consumption.MenuItemId,consumption.ConsumeDate,consumption.Quantity,consumption.Notes);
            }
            using (var cmd = new SqlCommand("SP_AddDailyConsumptions",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Consumption", tvpConsumption);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "ConsumeVar";
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }
    }
    public async Task<List<clsDailyConsumption>> GetAllDailyConsumptions()
    {
        using ( var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            var conList = new List<clsDailyConsumption>();
            using(var cmd = new SqlCommand("SP_GetAllDailyConsumptions",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var consumption = Mapper.MapDailyConsumption(reader);
                        conList.Add(consumption);
                    }
                    return conList;
                }
            }
        }
    }
    public async Task<List<clsDailyConsumption>> GetDailyConsumptionByDateAsync(DateTime conDate)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            var conList = new List<clsDailyConsumption>();
            using (var cmd = new SqlCommand("SP_GetDailyConsumptionByDate",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConsumeDate", conDate);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var consumption = Mapper.MapDailyConsumption(reader);
                        conList.Add(consumption);
                    }
                    return conList;
                }
            }
        }
    }
}
