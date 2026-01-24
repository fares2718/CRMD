using CRMD.Domain.PerchaseInvoices;

namespace CRMD.Application.Common.Interfaces
{
    public interface IPerchaseInvoiceRepository
    {
        Task AddPerchaseInvoiceAsync(PerchaseInvoice perchaseInvoice);
        Task DeletePerchaseInvoiceAsync(int id);
        Task<PerchaseInvoiceDto?> GetPerchaseInvoiceByDateAsync(DateTime date);
        Task<PerchaseInvoiceDto?> GetPerchaseInvoiceByIdAsync(int id);
        Task<List<PerchaseInvoiceItemDto>> GetPerchaseInvoiceItemsAsync(int invoiceId);
    }
}