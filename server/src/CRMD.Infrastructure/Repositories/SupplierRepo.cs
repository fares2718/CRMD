using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;
using System.Data;


namespace CRMD.Infrastructure.Repositories;

public class SupplierRepo : ISupplierRepo
{
    public async Task<string> AddSupplierAsync(clsSupplier supplier)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddSupplier", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@Address", supplier.Address);
                cmd.Parameters.AddWithValue("@Phone", supplier.Phone);
                cmd.Parameters.AddWithValue("@Email", supplier.Email);
                cmd.Parameters.AddWithValue("@Rating", supplier.Rating);
                var returnIdParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };   
                cmd.Parameters.Add(returnIdParam);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return returnIdParam.Value.ToString()!;
            }
        }
    }
    public async Task<bool> DeleteSupplierAsync(string supplierId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_DeleteSupplier", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", supplierId);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    public async Task<List<clsSupplier>> GetAllSuppliersAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetAllSuppliers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var suppliers = new List<clsSupplier>();
                await conn.OpenAsync();
                using (var reader = cmd.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        var supplier = Mapper.MapSupplier(reader);
                        suppliers.Add(supplier);
                    }
                }
                return suppliers;
            }
        }
    }
    public async Task<clsSupplier?> GetSupplierByIdAsync(int supplierId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetSupplierById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", supplierId);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var supplier = Mapper.MapSupplier(reader);
                        return supplier;
                    }
                }
                return null;
            }
        }
    }
    public async Task<List<clsPurchaseInvoice>> GetSupplierInvoices(string supplierId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_GetSupplierInvoices",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var supplierInvoices = new List<clsPurchaseInvoice>();
                cmd.Parameters.AddWithValue("@Id", supplierId);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var invoice = Mapper.MapInvoice(reader);
                        supplierInvoices.Add(invoice);
                    }  
                }
                return supplierInvoices;
            }
        }
    }
    public async Task<bool> RateSupplierAsync(string supplierId, decimal rating)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_RateSupplier", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", supplierId);
                cmd.Parameters.AddWithValue("@Rating", rating);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    public async Task<bool> UpdateSupplierPhoneAsync(string SupplierId,string newPhone)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdateSupplierPhone", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SupplierId);
                cmd.Parameters.AddWithValue("@Phone", newPhone);
                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    } 

}
