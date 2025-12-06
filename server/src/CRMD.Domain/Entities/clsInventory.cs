using System;

namespace CRMD.Domain.Entities;

public class clsInventory
{
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}
