namespace CRMD.Application.DTOs
{
    public class RecipeItemDto
    {
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; }
    }
}