using System.Collections.Generic;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask4.Web.Pages.Settings
{
    public class CreateCategoryModel : PageModel
    {
        public CategoryController CategoryController;
        public bool isAdded;
        public CreateCategoryModel(CategoryController categoryController)
        {
            CategoryController = categoryController;
            isAdded = false;
        }
        
        public async Task OnPostAsync(int categoryId, string subcategoryName)
        {
            isAdded = true;
            CategoryController.CurrentCategory = await CategoryController.AddChildAsync(categoryId, subcategoryName);//Создаем или добавляем подкатегорию
        }
        public void OnGet()
        {
        }
    }
}
