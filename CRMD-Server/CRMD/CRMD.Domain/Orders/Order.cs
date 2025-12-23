namespace CRMD.Domain.Orders;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int CaptainId { get; set; }
    public decimal TotalAmount { get; set; }
    public short OrderType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LeftAt { get; } = DateTime.Now;
    public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
}