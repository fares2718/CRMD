using System;

namespace CRMD.Domain.Entities;

public class clsPurchaseInvoiceItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
