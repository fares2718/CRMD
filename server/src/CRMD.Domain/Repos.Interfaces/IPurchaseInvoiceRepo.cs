using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IPurchaseInvoiceRepo
{
    public Task<int> AddPurchaseInvoiceAsync(clsPurchaseInvoice objPurchaseInvoice);
     public Task<int> AddPurchaseInvoiceItemsAsync(Queue<clsPurchaseInvoiceItem> purchaseInvoiceItems);
    public Task<bool> DeletePurchaseInvoiceAsync(int purchaseInvoiceId);
    public Task<List<clsPurchaseInvoice>> GetAllPurchaseInvoicesAsync();
    public Task<List<clsPurchaseInvoice>> GetPartialPurchaseInvoices();
    public Task<clsPurchaseInvoice?> GetPurchaseInvoiceByIdAsync(int purchaseInvoiceId);
    public Task<List<clsPurchaseInvoice>> GetUnPaidPurchaseInvoices();
    public Task<bool> UpdatePurchaseInvoiceNotesAsync(int InvoiceId,string Notes);
    public Task<bool> UpdatePurchaseInvoiceTotalAsync(int InvoiceId);
}
