namespace CRMD.Application.PerchaseInvoices;

public record AddPerchaseInvoiceCommand(
    int supplierId,
    decimal totalAmount,
    short paymentStatus,
    DateTime date,
    List<PerchaseInvoiceItemDto> invoiceItems
) : IRequest<ErrorOr<Created>>;