using CRMD.Domain.Attributes;

namespace CRMD.Domain.Items
{
    public class Item
    {
        [IgnoreOn(enOperationMode.Add)]
        int ItemId { get; set; }
        int CategoryId { get; set; }
        decimal Price { get; set; }
        string Name = null!;
    }
}