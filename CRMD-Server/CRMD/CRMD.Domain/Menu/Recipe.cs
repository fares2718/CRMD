namespace CRMD.Domain.Menu
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public List<RecipeItem> Ingredients { get; set; } = new List<RecipeItem>();
    }
}