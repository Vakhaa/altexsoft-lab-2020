using System;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class StepsInRecipe : BaseEntity
    {
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public Recipe Recipe { get; set; }
        public StepsInRecipe() { }
        public StepsInRecipe(int recipeId, string description)
        {
            if (recipeId != 0)
            {
                //Prowerki
                RecipeId = recipeId;
                Description = description;
            }
        }
    }
}
