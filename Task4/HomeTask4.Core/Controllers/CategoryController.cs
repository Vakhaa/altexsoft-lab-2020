using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    public class CategoryController
    {
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// Активная категория.
        /// </summary>
        public Category CurrentCategory { get; set; }
        public Category CurrentSubcategory { get; set; }

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Загрузка списка категорий в приложение.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository.ListAsync<Category>();
            return categories.Where(c => c.ParentId == null).ToList();
        }
        public async Task<List<Category>> GetSubcategoriesAsync()
        {
            var subcategories = await _unitOfWork.Repository.ListAsync<Category>();
            return subcategories.Where(c => c.ParentId != null).ToList();
        }
        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="nameSubcategory">Название новой подкатегории.</param>
        public async Task<Category> AddSubcategoryAsync(int categoryId, string nameSubcategory)
        {
            var subcategories = await GetSubcategoriesAsync();

            if (!int.TryParse(nameSubcategory, out int result))
            {
                if (!subcategories.Any(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId))
                {
                    CurrentSubcategory = new Category(nameSubcategory, categoryId);
                    await _unitOfWork.Repository.AddAsync(CurrentSubcategory);
                    return CurrentSubcategory;
                }
                else
                {
                    return CurrentSubcategory = subcategories.FirstOrDefault(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId);
                }
            }
            else
            {
                return null;
            }
        }
        public async Task<Category> AddCategoryAsync(string nameCategory)
        {
           return  await _unitOfWork.Repository.AddAsync(new Category(nameCategory));
        }
        /// <summary>
        /// Поиск категории.
        /// </summary>
        /// <param name="idCategory">Id категории.</param>
        public async Task FindCategoryAsync(int idCategory)
        {
            var categories = await GetCategoriesAsync();
            CurrentCategory = categories.FirstOrDefault(c => c.Id == idCategory);
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <param name="answer">Переменная, для ответа пользователя.</param>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        public async Task<bool> WalkCategoriesAsync(string answer)
        {
            if (int.TryParse(answer, out int result))
            {
                await FindCategoryAsync(result);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        /// <param name="answer">Параметр, для обработки ответа пользователя.</param>
        public async Task<bool> WalkSubcategoriesAsync(string answer)
        {
            if (int.TryParse(answer, out int result))
            {
                CurrentSubcategory = CurrentCategory.Children[result-1];
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Установка конкретной категории.
        /// </summary>
        /// <param name="answer">Переменная, для ответа пользователя.</param>
        public async Task SetCurrentCategoryAsync(string answer)
        {
            if (int.TryParse(answer, out int categoryId))
            {
                var categories = await GetCategoriesAsync();
                var category = categories.FirstOrDefault(category => category.Id == categoryId);
                if (category != null)
                {
                    CurrentCategory = category;
                }
            }
        } 
    }
}
