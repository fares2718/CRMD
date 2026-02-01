import { RecipeItem } from './recipe-item.model';

export interface Recipe {
  Items: RecipeItem[];
  totalCost: number;
}
