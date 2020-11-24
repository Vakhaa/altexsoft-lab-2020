using System.Collections.Generic;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeTask4.Web.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Ingredient> Ingredients;
        private IngredientController _ingredientController;
        public IndexModel(IngredientController ingredientsController)
        {
            _ingredientController = ingredientsController;
        }
        public async Task OnGetAsync()
        {
            Ingredients = await _ingredientController.GetIngredientsAsync();
        }
    }
}
