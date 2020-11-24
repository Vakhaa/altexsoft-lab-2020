using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask4.Web.Pages.Settings
{
    public class CreateIngredientModel : PageModel
    {
        public IngredientController IngredientController;
        public bool isAdded;
        public CreateIngredientModel(IngredientController ingredientController)
        {
            isAdded = false;
            IngredientController = ingredientController;
        }
        public void OnGet()
        {
        }
        public async Task OnPostAsync(string name)
        {
            isAdded = true;
            await IngredientController.AddedIfNewAsync(name);
        }
    }
}
