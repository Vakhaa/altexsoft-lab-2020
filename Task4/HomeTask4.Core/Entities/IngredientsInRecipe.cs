using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class IngredientsInRecipe : BaseEntity
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string CountIngredient { get; set; }
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        public IngredientsInRecipe() { }
        public IngredientsInRecipe(int recipeId, int ingredientId, string countIngredient)
        {
            if(recipeId!=0)
            {
                //Prowerki
                RecipeId = recipeId;
                IngredientId = ingredientId;
                CountIngredient = countIngredient;
            }
        }
    }
}
