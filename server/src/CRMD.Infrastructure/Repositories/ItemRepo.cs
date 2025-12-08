using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class ItemRepo : IItemsRepo
{
    public async Task<int> AddItemAsync(clsItem item)
    {
        using ( var conn = SqlConnectionFactory.CreateSqlConnection() )
        {
            using ( var cmd = new SqlCommand("SP_AddItem", conn) )
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Unit", item.Unit);
                cmd.Parameters.AddWithValue("@MinLevel", item.MinLevel);
                cmd.Parameters.AddWithValue("@Cost", item.Cost);

                var outputIdParam = new SqlParameter("@NewItemId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return (int)outputIdParam.Value;
            }
        }
    }

    public async Task<int> AddItemCategoryAsync(string category)
    {
        using ( var conn = SqlConnectionFactory.CreateSqlConnection() )
        {
            using ( var cmd = new SqlCommand("SP_AddItemCategory", conn) )
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", category);

                var outputIdParam = new SqlParameter("@NewCategoryId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return (int)outputIdParam.Value;
            }
        }
    }

    public async Task<bool> UpdateItemCostAsync(int itemId, decimal newCost)
    {
        using ( var conn = SqlConnectionFactory.CreateSqlConnection() )
        {
            using ( var cmd = new SqlCommand("SP_UpdateItemCost", conn) )
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@NewCost", newCost);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }
}
