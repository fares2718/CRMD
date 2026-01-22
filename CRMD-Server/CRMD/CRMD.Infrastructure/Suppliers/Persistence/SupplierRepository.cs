using CRMD.Domain.Suppliers;

namespace CRMD.Infrastructure.Suppliers.Persistence
{
    internal class SupplierRepository : ISupplierRepository
    {
        private readonly string _connectionString;

        public SupplierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddSupplier(Supplier supplier)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("external.addsupplier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, supplier.Name);
                    cmd.Parameters.AddWithValue("phones", NpgsqlDbType.Array | NpgsqlDbType.Varchar, supplier.Phones);
                    cmd.Parameters.AddWithValue("address", NpgsqlDbType.Varchar, supplier.Address);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteSupplier(int supplierId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("external.deletesupplier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", supplierId);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Supplier?> GetSupplierById(int supplierId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("select * from external.getsupplierbyid(@supplierid)", conn))
                {
                    cmd.Parameters.AddWithValue("supplierid", supplierId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            var supplier = Mapper.Map<Supplier>(reader);
                            return supplier;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            var suppliers = new List<Supplier>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("select * from external.getsuppliers()", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var supplier = Mapper.Map<Supplier>(reader);
                            suppliers.Add(supplier);
                        }
                    }
                }
            }
            return suppliers;
        }
    }
}