using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Domain.PerchaseInvoices
{
    public class PerchaseInvoiceItem
    {
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public decimal Quntity { get; set; }
        public decimal qtyPrice { get; set; }
    }
}