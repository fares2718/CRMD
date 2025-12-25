namespace CRMD.Infrastructure.EntityMapping
{
    public static class Mapper
    {
        public static T Map<T>(NpgsqlDataReader reader) where T : new()
        {
            T entity = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanWrite)
                    continue;
                try
                {
                    var value = reader[prop.Name];
                    if (value == DBNull.Value)
                        continue;
                    prop.SetValue(entity, Convert.ChangeType(value, prop.PropertyType));
                }
                catch
                {
                    // Ignore missing columns or conversion errors
                }
            }

            return entity;
        }

    }
}