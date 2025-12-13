using System;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;

namespace CRMD.Application.UseCases.Accountant;

public class RegisterPurchaseInvoiceUseCase
{
    private readonly IPurchaseInvoiceRepo _purchaseInvoiceRepo;

    public RegisterPurchaseInvoiceUseCase(IPurchaseInvoiceRepo purchaseInvoiceRepo)
    {
        _purchaseInvoiceRepo = purchaseInvoiceRepo;
    }
}
