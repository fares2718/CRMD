using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class PurchaseInvoiceRepo : IPurchaseInvoiceRepo
{
    public async Task<int> AddPurchaseInvoiceAsync(clsPurchaseInvoice objPurchaseInvoice)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddPurchaseInvoice", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", objPurchaseInvoice.SupplierId);
                cmd.Parameters.AddWithValue("@InvoiceDate", objPurchaseInvoice.InvoiceDate);
                cmd.Parameters.AddWithValue("@TotalAmount", objPurchaseInvoice.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentStatus", (int)objPurchaseInvoice.PaymentStatus);
                cmd.Parameters.AddWithValue("@Notes", objPurchaseInvoice.Notes);
                cmd.Parameters.AddWithValue("@DueDate", objPurchaseInvoice.DueDate);
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
    public async Task<bool> DeletePurchaseInvoiceAsync(int purchaseInvoiceId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_DeletePurchaseInvoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PurchaseInvoiceId", purchaseInvoiceId);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }    
    public async Task<List<clsPurchaseInvoice>> GetAllPurchaseInvoicesAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetAllPurchaseInvoices", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var purchaseInvoices = new List<clsPurchaseInvoice>();
                    while (await reader.ReadAsync())
                    {
                        var invoice = new clsPurchaseInvoice
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            SupplierId = reader.GetString(reader.GetOrdinal("SupplierId")),
                            InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            PaymentStatus = (clsPurchaseInvoice.enPaymentStatus)reader.GetInt32(reader.GetOrdinal("PaymentStatus")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                            RecevingDate = reader.GetDateTime(reader.GetOrdinal("RecevingDate")),
                            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("InvoiceNumber")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                        };
                        purchaseInvoices.Add(invoice);
                    }
                    return purchaseInvoices;
                }
            }
        }
    }
    public async Task<List<clsPurchaseInvoice>> GetPartialPurchaseInvoices()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetPartialPurchaseInvoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var purchaseInvoices = new List<clsPurchaseInvoice>();
                    while (await reader.ReadAsync())
                    {
                        var invoice = new clsPurchaseInvoice
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            SupplierId = reader.GetString(reader.GetOrdinal("SupplierId")),
                            InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            PaymentStatus = (clsPurchaseInvoice.enPaymentStatus)reader.GetInt32(reader.GetOrdinal("PaymentStatus")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                            RecevingDate = reader.GetDateTime(reader.GetOrdinal("RecevingDate")),
                            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("InvoiceNumber")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                        };
                        purchaseInvoices.Add(invoice);
                    }
                    return purchaseInvoices;
                }
            }
        }
    }
    public async Task<clsPurchaseInvoice?> GetPurchaseInvoiceByIdAsync(int purchaseInvoiceId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetPurchaseInvoiceById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PurchaseInvoiceId", purchaseInvoiceId);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var invoice = new clsPurchaseInvoice
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            SupplierId = reader.GetString(reader.GetOrdinal("SupplierId")),
                            InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            PaymentStatus = (clsPurchaseInvoice.enPaymentStatus)reader.GetInt32(reader.GetOrdinal("PaymentStatus")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                            RecevingDate = reader.GetDateTime(reader.GetOrdinal("RecevingDate")),
                            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("InvoiceNumber")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                        };
                        return invoice;
                    }
                    return null;
                }
            }
        }
    }
    public async Task<List<clsPurchaseInvoice>> GetUnPaidPurchaseInvoices()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetUnPaidPurchaseInvoice", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var purchaseInvoices = new List<clsPurchaseInvoice>();
                    while (await reader.ReadAsync())
                    {
                        var invoice = new clsPurchaseInvoice
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            SupplierId = reader.GetString(reader.GetOrdinal("SupplierId")),
                            InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            PaymentStatus = (clsPurchaseInvoice.enPaymentStatus)reader.GetInt32(reader.GetOrdinal("PaymentStatus")),
                            Notes = reader.GetString(reader.GetOrdinal("Notes")),
                            DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                            RecevingDate = reader.GetDateTime(reader.GetOrdinal("RecevingDate")),
                            InvoiceNumber = reader.GetInt32(reader.GetOrdinal("InvoiceNumber")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                        };
                        purchaseInvoices.Add(invoice);
                    }
                    return purchaseInvoices;
                }
            }
        }
    }
    public async Task<bool> UpdatePurchaseInvoiceNotesAsync(int InvoiceId, string Notes)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdatePurchaseInvoiceNotes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    public async Task<bool> UpdatePurchaseInvoiceTotalAsync(int InvoiceId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdatePurchaseInvoiceTotal", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

}