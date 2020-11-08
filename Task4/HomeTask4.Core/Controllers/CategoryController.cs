using System.Collections.Generic;
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
        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return _unitOfWork.Repository.GetWithIncludeListAsync<Category>(c => c.ParentId == null,c=>c.Children);
        }
        public Task<IEnumerable<Category>> GetAllChildAsync()
        {
            return _unitOfWork.Repository.GetWithIncludeListAsync<Category>(c => c.ParentId != null, c=>c.Parent);
        }
        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="nameSubcategory">Название новой подкатегории.</param>
        public async Task<Category> AddChildAsync(int categoryId, string nameSubcategory)
        {
            if (!int.TryParse(nameSubcategory, out int result))
            {
                var child = await _unitOfWork.Repository.GetWithIncludeEntityAsync<Category>(c => c.Name.ToLower() == nameSubcategory.ToLower() && c.ParentId == categoryId && c.ParentId != null, c => c.Parent);
                if (child == null)
                {
                    return CurrentCategory = await _unitOfWork.Repository.AddAsync(new Category(nameSubcategory, categoryId));
                }
                else
                {                    
                    return CurrentCategory = child;
                }
            }
            else
            {
                return null;
            }
        }
        public Task<Category> AddCategoryAsync(string nameCategory)
        {
           return _unitOfWork.Repository.AddAsync(new Category(nameCategory));
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
        public async Task SetCurrentCategoryAsync(int categoryId, IEnumerable<Category> categories = null)
        {
            CurrentCategory = await _unitOfWork.Repository.GetWithIncludeEntityAsync<Category>(c => c.Id == categoryId);
        }
    }
}
