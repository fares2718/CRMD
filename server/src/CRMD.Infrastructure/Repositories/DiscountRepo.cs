using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class DiscountRepo : IDiscountRepo
{
    public async Task<bool> ActivatePromocodeAsync(string promocode)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_ActivatePromocode", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Promocode", promocode);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<int> AddDiscountAsync(clsDiscount discount)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddDiscount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", discount.Name);
                cmd.Parameters.AddWithValue("@Value", discount.Value);
                cmd.Parameters.AddWithValue("@Type", discount.Type);
                cmd.Parameters.AddWithValue("@Promocode", discount.Promocode);
                cmd.Parameters.AddWithValue("@IsActive", discount.IsActive);
                var outputIdParam = new SqlParameter("@NewDiscountId", SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam); 
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return (int)outputIdParam.Value;
            }
        }
    }

    public async Task<bool> ApplyDiscountAsync(int discountId, int orderId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_ApplyDiscountToInvoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DiscountId", discountId);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> DeactivatePromocodeAsync(string promocode)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_DeactivatePromocode", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Promocode", promocode);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<List<clsDiscount>> GetActivePromocodesAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetActivePromocodes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var activePromocodes = new List<clsDiscount>();
                    while (await reader.ReadAsync())
                    {
                        var discount = Mapper.MapDiscount(reader);
                        activePromocodes.Add(discount);
                    }
                    return activePromocodes;
                }
            }
        }
    }

}
