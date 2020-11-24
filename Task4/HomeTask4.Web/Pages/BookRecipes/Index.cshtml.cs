using HomeTask4.Core.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Web.Pages.BookRecipes
{
    public class IndexModel : PageModel
    {
        private CategoryController _categoryController;
        private RecipeController _recipeController;
        private IngredientController _ingredientController;
        public IEnumerable<Category> CategoriesParent;
        public IEnumerable<Recipe> Recipes;
        public IEnumerable<Category> CategoriesChild;
        public IndexModel(CategoryController categoryController,RecipeController recipeController, IngredientController ingredientController)
        {
            _categoryController = categoryController;
            _recipeController = recipeController;
            _ingredientController = ingredientController;
        }
        public async Task OnGetAsync()
        {
            CategoriesParent = await _categoryController.GetCategoriesAsync();
            CategoriesChild = await _categoryController.GetAllChildAsync();
            Recipes = await _recipeController.GetRecipesAsync();
        }

    }
}
