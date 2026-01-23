using CRMD.Domain.PerchaseInvoices;

namespace CRMD.Application.Common.Interfaces
{
    public interface IPerchaseInvoiceRepository
    {
        Task AddPerchaseInvoiceAsync(PerchaseInvoice perchaseInvoice);
        Task DeletePerchaseInvoice(int id);
        Task<PerchaseInvoiceDto?> GetPerchaseIncoiceByDate(DateTime date);
        Task<PerchaseInvoiceDto?> GetPerchaseIncoiceById(int id);
        Task<List<PerchaseInvoiceItemDto>> GetPerchaseInvoiceItems(int invoiceId);
    }
}