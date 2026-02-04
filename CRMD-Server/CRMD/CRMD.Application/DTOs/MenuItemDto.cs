namespace CRMD.Application.DTOs
{
    public class MenuItemDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int RecipeId { get; set; }
        public List<int> Ingrediants { get; set; } = new List<int>();
    }
}