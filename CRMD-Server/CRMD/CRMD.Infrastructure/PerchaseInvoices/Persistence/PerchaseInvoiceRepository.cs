using CRMD.Domain.PerchaseInvoices;

namespace CRMD.Infrastructure.PerchaseInvoices.Persistence
{
    public class PerchaseInvoiceRepository : IPerchaseInvoiceRepository
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
    }
}