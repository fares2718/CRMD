using System.Transactions;
using CRMD.Domain.PerchaseInvoices;

namespace CRMD.Infrastructure.PerchaseInvoices.Persistence
{
    internal class PerchaseInvoiceRepository : IPerchaseInvoiceRepository
    {
        private readonly string _connectionString;

        public PerchaseInvoiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddPerchaseInvoiceAsync(PerchaseInvoice perchaseInvoice)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("external.addperchaseinvoice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("supplierid", perchaseInvoice.SupplierId);
                    cmd.Parameters.Add("totalamount", NpgsqlDbType.Money).Value = perchaseInvoice.TotalAmount;
                    cmd.Parameters.AddWithValue("date", perchaseInvoice.Date);
                    var invoiceItemsJson = JsonSerializer.Serialize(perchaseInvoice.InvoiceItems);
                    cmd.Parameters.AddWithValue("invoiceitems", NpgsqlDbType.Jsonb,
                    invoiceItemsJson);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeletePerchaseInvoice(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("external.deleteperchaseinvoice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("invoiceid", id);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<PerchaseInvoiceDto?> GetPerchaseIncoiceByDate(DateTime date)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("select * from external.getperchaseinvoicebydate(@invoicedate)", conn))
                {
                    cmd.Parameters.AddWithValue("invoicedate", date);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            var invoice = Mapper.Map<PerchaseInvoiceDto>(reader);
                            return invoice;
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<PerchaseInvoiceDto?> GetPerchaseIncoiceById(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("select * from external.getperchaseinvoicebyid(@invoiceid)", conn))
                {
                    cmd.Parameters.AddWithValue("invoiceid", id);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            var invoice = Mapper.Map<PerchaseInvoiceDto>(reader);
                            return invoice;
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<List<PerchaseInvoiceItemDto>> GetPerchaseInvoiceItems(int invoiceId)
        {
            var items = new List<PerchaseInvoiceItemDto>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {

                using (var cmd = new NpgsqlCommand("SELECT * FROM external.getperchaseinvoiceitems(@invoiceid)", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("invoiceid", invoiceId);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var item = Mapper.Map<PerchaseInvoiceItemDto>(reader);
                            items.Add(item);
                        }
                    }
                }
            }
            return items;
        }


    }
}