using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask4.Web.Pages.BookRecipes
{
    public class OpenRecipeModel : PageModel
    {
        public Recipe Recipe;
        private RecipeController _recipeController;
        private IngredientController _ingredientController;
        public OpenRecipeModel(RecipeController recipeController, IngredientController ingredientController)
        {
            _recipeController = recipeController;
            _ingredientController = ingredientController;
        }
        public async Task OnGetAsync(int id)
        {
            Recipe = await _recipeController.FindRecipeAsync(id);
        }
        public async Task<Ingredient> GetIngredientAsync(int id)
        {
            return await _ingredientController.GetIngredientByIdAsync(id);
        }
    }
}
