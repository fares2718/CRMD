using System;

namespace CRMD.Domain.Entities;

public class clsPayment
{
    public string Id { get; set;  } = null!;
    public string SupplierId { get; set; } = null!;
    public int InvoiceId { get; set;  }
    public decimal AmountPaid { get; set;  }
    public DateTime PaymentDate { get; set;  }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set;  }
}
