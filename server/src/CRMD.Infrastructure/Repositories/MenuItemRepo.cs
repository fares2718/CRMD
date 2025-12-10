using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class MenuItemRepo : IMenuItemRepo
{
    public async Task<int> AddMenuItemAsync(clsMenuItem menuItem)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddMenuItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", menuItem.Name);
                cmd.Parameters.AddWithValue("@MenuItemCategoryId", menuItem.CategoryId);
                cmd.Parameters.AddWithValue("@Price", menuItem.Price);
                cmd.Parameters.AddWithValue("@IsAvailable", menuItem.IsAvailable);
                cmd.Parameters.AddWithValue("@CreatedAt", menuItem.CreatedAt);

                var outputIdParam = new SqlParameter("SCOPE_IDENTITY()", SqlDbType.Int)
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

    public async Task<bool> AvailableMenuItemAsync(int menuItemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AvailableMenuItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<List<clsMenuItem>> GetAllMenuItemsAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetAllMenuItems", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var menuItems = new List<clsMenuItem>();
                    while (await reader.ReadAsync())
                    {
                        var menuItem = Mapper.MapMenuItem(reader);
                        menuItems.Add(menuItem);
                    }
                    return menuItems;
                }
            }
        }
    }

    public async Task<clsMenuItem?> GetMenuItemByIdAsync(int menuItemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetMenuItemById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return Mapper.MapMenuItem(reader);
                    }
                    return null;
                }
            }
        }
    }

    public async Task<bool> UnavailableMenuItemAsync(int menuItemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UnavailableMenuItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> UpdateMenuItemPriceAsync(int menuItemId, decimal newPrice)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdateMenuItemPrice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);
                cmd.Parameters.AddWithValue("@NewPrice", newPrice);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }
}
