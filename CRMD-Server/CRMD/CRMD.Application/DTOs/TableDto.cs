namespace CRMD.Application.DTOs
{
    public class TableDto
    {
        public int TableId { get; set; }
        public int SectionId { get; set; }
        public string Waiter { get; set; } = string.Empty;
        public short Capacity { get; set; }
    }
}