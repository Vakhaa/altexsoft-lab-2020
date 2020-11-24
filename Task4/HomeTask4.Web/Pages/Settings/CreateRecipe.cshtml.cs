using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask4.Web.Pages.Settings
{
    public class CreateRecipeModel : PageModel
    {
        public CategoryController CategoryController;
        public RecipeController RecipeController;
        public IngredientController IngredientController;
        public CreateRecipeModel(CategoryController categoryController, RecipeController recipeController, IngredientController ingredientController)
        {
            CategoryController = categoryController;
            RecipeController = recipeController;
            IngredientController = ingredientController;
        }
        public async Task OnPostAsync(string recipeName, string categoryId, string description, List<string> ingredients, List<string> countIngredients, List<string> stepsHowCooking)
        {
            await CategoryController.WalkCategoriesAsync(categoryId);
            var categoryNew = CategoryController.CurrentCategory;
            if (categoryNew == null)
            {
                throw new ArgumentException(nameof(categoryNew), "Null categoryNew in new Recipe");
            }
            
            var ingredientsId = new List<int>();
            foreach(var name in ingredients)
            {
                   ingredientsId.Add(await IngredientController.AddedIfNewAsync(name));
            }

            await RecipeController.CreateRecipeAsync(recipeName, categoryNew.Id, description);
            await RecipeController.AddedIngredientsInRecipeAsync(ingredientsId, countIngredients);
            await RecipeController.AddedStepsInRecipeAsync(stepsHowCooking);
        }
        public void OnGet()
        {
        }
    }
}
