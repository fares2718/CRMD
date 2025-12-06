using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IPurchaseInvoiceItemRepo
{
    public Task<int> AddPurchaseInvoiceItemsAsync(Queue<clsPurchaseInvoiceItem> purchaseInvoiceItems);
}
