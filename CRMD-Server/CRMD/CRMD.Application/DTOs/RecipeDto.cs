namespace CRMD.Application.DTOs
{
    public class RecipeDto
    {
        public List<RecipeItemDto> Items { get; set; } =
        new List<RecipeItemDto>();
        public decimal TotalCost { get; }
    }
}