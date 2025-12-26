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
                cmd.Parameters.AddWithValue("tableid", order.TableId);
                cmd.Parameters.AddWithValue("captainid", order.CaptainId);
                cmd.Parameters.Add("totalamount", NpgsqlDbType.Money).Value = order.TotalAmount;
                cmd.Parameters.AddWithValue("ordertype", order.OrderType);
                cmd.Parameters.AddWithValue("createdat", order.CreatedAt);
                cmd.Parameters.AddWithValue("leftat", order.LeftAt);
                var orderItemsJson = JsonSerializer.Serialize(order.OrderItems);
                cmd.Parameters.AddWithValue("orderitems",
                orderItemsJson);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<Order>> GetOrdersByDateAsync(DateTime date)
    {
        var orders = new List<Order>();
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("select * from getordersbydate(@orderdate)", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("orderdate", date);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var order = Mapper.Map<Order>(reader);
                        order.OrderItems = JsonSerializer.Deserialize<List<OrderItems>>(
                            reader.GetString(reader.GetOrdinal("orderitems")))!;
                        orders.Add(order);
                    }
                }
            }
        }
        return orders;
    }



}