using CRMD.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Mappers;

public class Mapper
{
    public static clsSupplier MapSupplier(SqlDataReader reader)
    {
        var supplier = new clsSupplier
                        {
                            SupplierId = reader.GetString(reader.GetOrdinal("Id")),
                            SupplierName = reader.GetString(reader.GetOrdinal("Name")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Rating = reader.GetDecimal(reader.GetOrdinal("Rating")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        };
        return supplier;
    }

    public static clsPurchaseInvoice MapInvoice(SqlDataReader reader)
    {
        var invoice = new clsPurchaseInvoice
                        {
                          Id = reader.GetInt32(reader.GetOrdinal("Id")),
                          SupplierId = reader.GetString(reader.GetOrdinal("SupplierId")),
                          CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                          InvoiceNumber = reader.GetInt32(reader.GetOrdinal("InvoiceNumer")),
                          TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                          PaymentStatus = (clsPurchaseInvoice.enPaymentStatus)
                          reader.GetInt16(reader.GetOrdinal("PaymentStatus")),
                          InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate")),
                          RecevingDate = reader.GetDateTime(reader.GetOrdinal("RecevingDate")),
                          DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                          UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                          Notes = reader.GetString(reader.GetOrdinal("Notes"))
                        };
        return invoice;
    }

    public static clsPurchaseInvoiceItem MapInvoiceItem(SqlDataReader reader)
    {
        var item = new clsPurchaseInvoiceItem
                       {
                           Id = reader.GetInt32(reader.GetOrdinal("Id")),
                           InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId")),
                           Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                           UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                           TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                           ItemId = reader.GetInt32(reader.GetOrdinal("ItemId"))
                       };
        return item;
    }

    public static clsInventory MapInventoryItem(SqlDataReader reader)
    {
        var item = new clsInventory
                       {
                           ItemId = reader.GetInt32(reader.GetOrdinal("ItemId")),
                           Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                           LastUpdated = reader.GetDateTime(reader.GetOrdinal("LastUpdated"))
                       };
        return item;
    }

    public static clsInventoryTransaction MapInventoryTransaction(SqlDataReader reader)
    {
        var transaction = new clsInventoryTransaction
                              {
                                  Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                  ItemId = reader.GetInt32(reader.GetOrdinal("ItemId")),
                                  TransactionType = (clsInventoryTransaction.enTransactionType)
                                  reader.GetInt16(reader.GetOrdinal("TransactionType")),
                                  TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                                  ReferenceId = reader.GetInt32(reader.GetOrdinal("ReferenceId"))
                              };
        return transaction;
    }

    public static clsDailyConsumption MapDailyConsumption(SqlDataReader reader)
    {
        var consumption = new clsDailyConsumption
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            MenuItemId = reader.GetInt32(reader.GetOrdinal("MenuItemId")),
            Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
            ConsumeDate = reader.GetDateTime(reader.GetOrdinal("ConsumeDate")),
            Notes = reader.GetString(reader.GetOrdinal("Notes"))
        };
        return consumption;
    }

    public static clsTable MapTable(SqlDataReader reader)
    {
        var table = new clsTable
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("TableName")),
            WaiterId = reader.GetString(reader.GetOrdinal("WaiterId")),
            Status = reader.GetInt16(reader.GetOrdinal("Status"))
        };
        return table;
    }
}
