using System.Data;
using CRMD.Application.Common.Interfaces;
using CRMD.Domain.Orders;
using Npgsql;
using NpgsqlTypes;

namespace CRMD.Infrastructure.Orders.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task AddOrderAsync(Order order)
    {
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("addorder", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tableid",order.TableId);
                cmd.Parameters.AddWithValue("captainid", order.CaptainId);
                cmd.Parameters.Add("totalamount",NpgsqlDbType.Money).Value=order.TotalAmount;
                cmd.Parameters.AddWithValue("ordertype",order.OrderType);
                cmd.Parameters.Add("createdat",NpgsqlDbType.Timestamp).Value=order.CreatedAt;
                cmd.Parameters.Add("leftat",NpgsqlDbType.Timestamp).Value=order.LeftAt;
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}