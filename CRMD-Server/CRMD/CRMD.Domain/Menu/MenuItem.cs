using CRMD.Domain.Attributes;

namespace CRMD.Domain.Menu
{
    public class MenuItem
    {
        [IgnoreOn(enOperationMode.Add)]
        public int MenuItemId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public List<RecipeItem> Ingredients { get; set; } = new List<RecipeItem>();
        public short CategoryId { get; set; }
    }
}