using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask4.Web.Pages.Settings
{
    public class CreateIngredientModel : PageModel
    {
        private IngredientController _ingredientController;
        public bool isAdded;
        public CreateIngredientModel(IngredientController ingredientController)
        {
            isAdded = false;
            _ingredientController = ingredientController;
        }
        public void OnGet()
        {
        }
        public async Task OnPostAsync(string name)
        {
            isAdded = true;
            await _ingredientController.AddedIfNewAsync(name);
        }
    }
}
