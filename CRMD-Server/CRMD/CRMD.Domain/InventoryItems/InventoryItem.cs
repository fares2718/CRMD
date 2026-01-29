using CRMD.Domain.Attributes;

namespace CRMD.Domain.InventoryItems
{
    public class InventoryItem
    {
        int ItemId { get; set; }
        decimal Quantity { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        decimal MinLevel { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        [IgnoreOn(enOperationMode.Add)]
        DateTime CreatedAt { get; set; }
        [IgnoreOn(enOperationMode.Add)]
        [IgnoreOn(enOperationMode.Update)]
        DateTime UpdatedAt { get; set; }
    }
}