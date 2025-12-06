using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class InventoryRepo : IInventoryRepo
{
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
                        var item = new clsInventory
                        {
                            ItemId = reader.GetInt32(reader.GetOrdinal("ItemId")),
                            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                            LastUpdated = reader.GetDateTime(reader.GetOrdinal("LastUpdated"))
                        };
                        inventoryItems.Add(item);
                    }
                }

                return inventoryItems;
            }
        }
    }
}
