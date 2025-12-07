using System;

namespace CRMD.Domain.Entities;

public class clsOrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string Notes { get; set; } = string.Empty;
}
