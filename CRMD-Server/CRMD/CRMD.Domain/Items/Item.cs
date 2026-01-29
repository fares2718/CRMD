using CRMD.Domain.Attributes;

namespace CRMD.Domain.Items
{
    public class Item
    {
        [IgnoreOn(enOperationMode.Add)]
        public int ItemId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public string Name = null!;
    }
}