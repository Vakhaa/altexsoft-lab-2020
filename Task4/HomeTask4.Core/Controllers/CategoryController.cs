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

        public int LevelImmersion;

        public  CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LevelImmersion = 0;
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
        public async Task<List<Category>> GetAllChildAsync()
        {
            var subcategories = await _unitOfWork.Repository.ListAsync<Category>();
            return subcategories.Where(c => c.ParentId != null).ToList();
        }
        public async Task<List<Category>> GetCurrentChildAsync()
        {
            var subcategories = await _unitOfWork.Repository.ListAsync<Category>();
            return subcategories.Where(c => c.ParentId == CurrentCategory.Id).ToList();
        }

        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="nameSubcategory">Название новой подкатегории.</param>
        public async Task<Category> AddChildAsync(int categoryId, string nameSubcategory)
        {
            var child = await GetAllChildAsync();

            if (!int.TryParse(nameSubcategory, out int result))
            {
                if (!child.Any(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId))
                {
                    return CurrentCategory = await _unitOfWork.Repository.AddAsync(new Category(nameSubcategory, categoryId));
                }
                else
                {
                    return CurrentCategory = child.FirstOrDefault(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId);
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
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <param name="answer">Переменная, для ответа пользователя.</param>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        public async Task<bool> WalkCategoriesAsync(string answer)
        {
            if (int.TryParse(answer, out int categoryId))
            {
                await SetCurrentCategoryAsync(categoryId);
                    if (CurrentCategory == null) //Если пользователь дал неверный id дочерней категории
                        return false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task SetCurrentCategoryAsync(int categoryId, List<Category> categories = null)
        {
            categories = await GetCategoriesAsync();
            categories.AddRange(await GetAllChildAsync());
            CurrentCategory = categories.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
