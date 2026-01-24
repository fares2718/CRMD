using CRMD.Domain.Suppliers;
using CRMD.Infrastructure.Generics;

namespace CRMD.Infrastructure.Suppliers.Persistence
{
    internal class SupplierRepository : ISupplierRepository
    {
        private readonly string _connectionString;

        public SupplierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
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
            }*/
            await GenericRepository<Supplier>.AddAsync(supplier, _connectionString, "external.addsupplier");
        }

        public async Task DeleteSupplierAsync(int Id)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
            {
                using (var cmd = new NpgsqlCommand("external.deletesupplier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", supplierId);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }*/
            await GenericRepository<Supplier>.DeleteAsync(Id, _connectionString, "external.deletesupplier");
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int supplierId)
        {
            /*using (var conn = new NpgsqlConnection(_connectionString))
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
            return null;*/
            using (var reader = await GenericRepository<Supplier>
            .GetByIdAsync(supplierId, _connectionString, "external.getsupplierbyid(@supplierid)"))
            {
                Supplier supplier = new Supplier();
                if (reader != null && await reader.ReadAsync())
                {
                    supplier = Mapper.Map<Supplier>(reader);
                }
                NpgsqlConnection.ClearAllPools();
                return supplier;
            }
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            var suppliers = new List<Supplier>();
            /*using (var conn = new NpgsqlConnection(_connectionString))
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
            return suppliers;*/
            using (var reader = await GenericRepository<Supplier>
            .GetAllAsync(_connectionString, "external.getsuppliers(@supplierid)"))
            {
                while (await reader.ReadAsync())
                {
                    var supplier = Mapper.Map<Supplier>(reader);
                    suppliers.Add(supplier);
                }
                NpgsqlConnection.ClearAllPools();
                return suppliers;
            }
        }
    }
}