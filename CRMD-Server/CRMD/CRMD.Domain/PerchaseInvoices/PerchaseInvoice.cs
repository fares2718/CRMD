using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Domain.PerchaseInvoices
{
    public class PerchaseInvoice
    {
        public int InvoiceId { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public short PaymentStatus { get; set; }
        public List<PerchaseInvoiceItem> InvoiceItems { get; set; } = new List<PerchaseInvoiceItem>();
    }
}