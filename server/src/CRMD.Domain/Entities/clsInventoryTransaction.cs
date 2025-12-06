using System;

namespace CRMD.Domain.Entities;

public class clsInventoryTransaction
{
    public enum enTransactionType
    {
        Addition = 0,
        Removal = 1,
        Adjustment = 2
    }
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int ReferenceId { get; set; }
    public DateTime TransactionDate { get; set; }
    public enTransactionType TransactionType { get; set; }
}
