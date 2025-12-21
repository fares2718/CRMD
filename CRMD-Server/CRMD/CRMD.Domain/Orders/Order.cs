namespace CRMD.Domain.Orders;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public decimal Price { get; set; }
    public short OrderType { get; set; }
    private List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
}