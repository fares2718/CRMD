namespace CRMD.Domain.Entities;

public class clsPurchaseInvoice
{
    public enum enPaymentStatus
    {
        UnPaid = 0,
        Partial = 1,
        Paid = 2
    }
    public int Id { get; set; }
    public string SupplierId { get; set; } = null!;
    public int InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set;} = DateTime.UtcNow;
    public DateTime RecevingDate { get; set;} = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public enPaymentStatus PaymentStatus { get; set;}
    public string Notes {get; set;} = string.Empty;
    public DateTime CreatedAt { get; set;} = DateTime.UtcNow;
    public DateTime DueDate { get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;
}
