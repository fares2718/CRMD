using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Application.DTOs
{
    public class PerchaseInvoiceDto
    {
        public int InvoiceId { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public short PaymentStatus { get; set; }
        public List<PerchaseInvoiceItemDto> InvoiceItems { get; set; } = new List<PerchaseInvoiceItemDto>();
    }
}