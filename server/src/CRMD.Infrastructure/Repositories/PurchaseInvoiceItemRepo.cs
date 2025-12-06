using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class PurchaseInvoiceItemRepo : IPurchaseInvoiceItemRepo
{
    public async Task<int> AddPurchaseInvoiceItemsAsync(Queue<clsPurchaseInvoiceItem> purchaseInvoiceItems)
    {
        DataTable purchaseInvoiceItemsTable = new DataTable();
        purchaseInvoiceItemsTable.Columns.Add("InvoiceId", typeof(int));
        purchaseInvoiceItemsTable.Columns.Add("ItemId", typeof(int));
        purchaseInvoiceItemsTable.Columns.Add("Quantity", typeof(decimal)); 
        purchaseInvoiceItemsTable.Columns.Add("UnitPrice", typeof(decimal));
        purchaseInvoiceItemsTable.Columns.Add("TotalPrice", typeof(decimal));
        while (purchaseInvoiceItems.Count > 0)
        {
            clsPurchaseInvoiceItem item = purchaseInvoiceItems.Dequeue();
            purchaseInvoiceItemsTable.Rows.Add(item.PurchaseInvoiceId, item.ItemId, item.Quantity, item.UnitPrice, item.TotalPrice);
        }
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddPurchaseInvoiceItems", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", purchaseInvoiceItemsTable);
                tvpParam.SqlDbType = SqlDbType.Structured;
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }
    }
    
}
