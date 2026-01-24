using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMD.Domain.Attributes;

namespace CRMD.Domain.PerchaseInvoices
{
    public class PerchaseInvoice
    {
        [IgnoreOn(enOperationMode.Add)]
        public int InvoiceId { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public short PaymentStatus { get; set; }
        public List<PerchaseInvoiceItem> InvoiceItems { get; set; } = new List<PerchaseInvoiceItem>();
    }
}