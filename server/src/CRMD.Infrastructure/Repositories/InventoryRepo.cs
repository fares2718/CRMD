using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class InventoryRepo : IInventoryRepo
{
    public async Task AddInventoryItemAsync(clsInventory objInventory)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddInventoryItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemId", objInventory.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", objInventory.Quantity); 
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                
            }
        }
    }
    public async Task<int> AddInventoryTransactionAsync(clsInventoryTransaction objInventoryTransaction)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddInventoryTransaction", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemId", objInventoryTransaction.ItemId);
                cmd.Parameters.AddWithValue("@ReferenceId", objInventoryTransaction.ReferenceId);
                cmd.Parameters.AddWithValue("@TransactionType", (int)objInventoryTransaction.TransactionType);
                var returnIdParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };   
                cmd.Parameters.Add(returnIdParam);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return (int)returnIdParam.Value;
            }
        }
    }
    public async Task<List<clsInventory>> GetAllInventrotyItemsAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetAllInventoryItems", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                var inventoryItems = new List<clsInventory>();

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var item = Mapper.MapInventoryItem(reader);
                        inventoryItems.Add(item);
                    }
                }

                return inventoryItems;
            }
        }
    }
    public async Task<clsInventory?> GetInventoryItemByIdAsync(int itemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetInventoryItemId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemId", itemId);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var item = Mapper.MapInventoryItem(reader);
                        return item;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
    public async Task<List<clsInventoryTransaction>> GetInventoryTransactionByItemAsync(int ItemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetInventoryTransactionsByItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemId", ItemId);

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var transactions = new List<clsInventoryTransaction>();
                    while (await reader.ReadAsync())
                    {
                        var transaction = Mapper.MapInventoryTransaction(reader);
                        transactions.Add(transaction);
                    }
                    return transactions;
                }
            }
        }
    }

}
