using CRMD.Contracts.PerchaseInvoices;

namespace CRMD.Contracts.PurchaseInvoices.Post;

public record AddPerchaseInvoiceRequest(
    int supplierId,
    decimal totalAmount,
    enPaymentStatus paymentStatus,
    DateTime date,
    List<PerchaseInvoiceItemDto> invoiceItems
);