using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Mappers;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class OrderRepo : IOrderRepo
{
    public async Task<int> AddOrderItemAsync(Queue<clsOrderItem> orderItems)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            DataTable tvpOrderItems = new DataTable();
            tvpOrderItems.Columns.Add("OrderId", typeof(int));
            tvpOrderItems.Columns.Add("MenuItemId", typeof(int));
            tvpOrderItems.Columns.Add("Quantity", typeof(decimal));
            tvpOrderItems.Columns.Add("Price", typeof(decimal));
            tvpOrderItems.Columns.Add("Notes", typeof(string));
            while(orderItems.Count > 0)
            {
                var item = orderItems.Dequeue();
                tvpOrderItems.Rows.Add(item.OrderId, item.MenuItemId, item.Quantity, item.Price, item.Notes);
            }
            using(var cmd = new SqlCommand("SP_AddOrderItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var tvpParam = cmd.Parameters.AddWithValue("@OrderItems", tvpOrderItems);
                tvpParam.SqlDbType = SqlDbType.Structured;  
                tvpParam.TypeName = "OrderItemsVar";
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected;
            }
        }
    }

    public async Task<int> CreateOrderAsync(clsOrder order)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using(var cmd = new SqlCommand("SP_CreateOrder", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TableId", order.TableId);
                cmd.Parameters.AddWithValue("@WaiterId", order.WaiterId);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                cmd.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);
                var outputIdParam = new SqlParameter("@NewOrderId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputIdParam);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return (int)outputIdParam.Value;
            }
        }
    }

    public async Task<bool> DeleteOrderItemAsync(int orderItemId, int orderId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using(var cmd = new SqlCommand("SP_DeleteOrderItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderItemId", orderItemId);
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> FinalizeOrderAsync(int orderId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using(var cmd = new SqlCommand("SP_FinalizeOrder", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }

    public async Task<clsOrder?> GetOrderByIdAsync(int orderId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using(var cmd = new SqlCommand("SP_GetOrderById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                await conn.OpenAsync();
                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var order = Mapper.MapOrder(reader);
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }

    public async Task<List<clsOrderItem>> GetOrderItemsByAsync(int orderId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using(var cmd = new SqlCommand("SP_GetOrderItemsByOrderId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                await conn.OpenAsync();
                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    var orderItems = new List<clsOrderItem>();
                    while (await reader.ReadAsync())
                    {
                        var item = Mapper.MapOrderItem(reader);
                        orderItems.Add(item);
                    }
                    return orderItems;
                }
            }
        }
    }
}
