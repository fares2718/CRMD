using CRMD.Domain.PerchaseInvoices;

namespace CRMD.Application.Common.Interfaces
{
    public interface IPerchaseInvoiceRepository
    {
        public Task AddPerchaseInvoiceAsync(PerchaseInvoice perchaseInvoice);

    }
}