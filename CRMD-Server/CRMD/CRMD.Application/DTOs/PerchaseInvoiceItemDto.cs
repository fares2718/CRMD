namespace CRMD.Application.DTOs
{
    public class PerchaseInvoiceItemDto
    {
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public decimal Quntity { get; set; }
        public decimal qtyPrice { get; set; }
    }
}