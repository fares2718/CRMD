using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Persistence.Databases;

public class SqlConnectionFactory
{
    private static string _connectionString = @"Server=localhost;Database=CRMD;Trusted_Connection=True;
                                                    User=sa;Password=Sherlock@71";
    public static SqlConnection CreateSqlConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
