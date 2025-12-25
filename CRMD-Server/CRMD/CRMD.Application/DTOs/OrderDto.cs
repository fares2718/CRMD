namespace CRMD.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int CaptainId { get; set; }
        public decimal TotalAmount { get; set; }
        public short OrderType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LeftAt { get; } = DateTime.Now;
        public List<OrderItemsDto> OrderItems { get; set; } = new List<OrderItemsDto>();
    }
}