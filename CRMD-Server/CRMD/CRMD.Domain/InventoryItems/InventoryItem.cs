namespace CRMD.Domain.InventoryItems
{
    public class InventoryItem
    {
        int ItemId { get; set; }
        decimal Quantity { get; set; }
        decimal MinLevel { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}