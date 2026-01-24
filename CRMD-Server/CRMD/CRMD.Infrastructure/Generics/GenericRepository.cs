using System.Reflection;
using CRMD.Domain;
using CRMD.Domain.Attributes;

namespace CRMD.Infrastructure.Generics
{
    internal static class GenericRepository<T> where T : class
    {

        public static async Task AddAsync(T entity, string connectionString, string procedureName)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var cmd = new NpgsqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        var ignoreAttrs = prop.GetCustomAttributes<IgnoreOnAttribute>();
                        if (ignoreAttrs.Any(a => a.operationMode == enOperationMode.Add))
                            continue;

                        var value = prop.GetValue(entity) ?? DBNull.Value;
                        var dbType = _MapType(prop.PropertyType);

                        var param = new NpgsqlParameter
                        {
                            ParameterName = prop.Name.ToLower(),
                            Value = value,
                            NpgsqlDbType = dbType
                        };

                        cmd.Parameters.Add(param);

                    }
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteAsync(int Id, string connectionString, string procedureName)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var cmd = new NpgsqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", Id);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<NpgsqlDataReader> GetAllAsync(string connectionString, string functionName)
        {
            var conn = new NpgsqlConnection(connectionString);

            var cmd = new NpgsqlCommand($"SELECT * FROM {functionName}", conn);
            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            return reader;

        }

        public static async Task<NpgsqlDataReader?> GetByIdAsync(int Id, string connectionString, string functionName)
        {
            var conn = new NpgsqlConnection(connectionString);

            var cmd = new NpgsqlCommand($"select * from {functionName}", conn);
            cmd.Parameters.AddWithValue("id", Id);
            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();
            return reader;
        }


        private static NpgsqlDbType _MapType(Type type)
        {
            if (type == typeof(int)) return NpgsqlDbType.Integer;
            if (type == typeof(short)) return NpgsqlDbType.Smallint;
            if (type == typeof(short[])) return NpgsqlDbType.Array | NpgsqlDbType.Smallint;
            if (type == typeof(string)) return NpgsqlDbType.Varchar;
            if (type == typeof(string[])) return NpgsqlDbType.Array | NpgsqlDbType.Varchar;
            if (type == typeof(bool)) return NpgsqlDbType.Boolean;
            if (type == typeof(DateTime)) return NpgsqlDbType.TimestampTz;
            if (type == typeof(decimal)) return NpgsqlDbType.Numeric;

            return NpgsqlDbType.Text; // fallback
        }
    }
}