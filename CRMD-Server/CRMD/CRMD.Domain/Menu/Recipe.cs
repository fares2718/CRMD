using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Domain.Menu
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public decimal Cost { get; set; }
        public List<RecipeItem> Ingredients { get; set; } = new List<RecipeItem>();
    }
}