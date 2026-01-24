using CRMD.Domain.Attributes;

namespace CRMD.Domain.Items
{
    public class Item
    {
        [IgnoreOn(enOperationMode.Add)]
        int ItemId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        int CategoryId { get; set; }
        decimal Price { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        string Name = null!;
    }
}