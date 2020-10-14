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
        
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Загрузка списка категорий в приложение.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public List<Category> GetCategories()
        {
            return _unitOfWork.Repository.ListAsync<Category>().GetAwaiter().GetResult().Where(c => c.ParentId == null).ToList();
        }
        /// <summary>
        /// Сохранение категорий.
        /// </summary>
        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<Category> AddCategoryAsync(string nameCategory)
        {
           return  await _unitOfWork.Repository.AddAsync(new Category(nameCategory));
        }
        /// <summary>
        /// Поиск категории.
        /// </summary>
        /// <param name="idCategory">Id категории.</param>
        public void FindCategory(int idCategory)
        {
             CurrentCategory = GetCategories().FirstOrDefault(c => c.Id == idCategory);
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <param name="str">Переменная, для ответа пользователя.</param>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        public bool WalkCategories(string str)
        {
            if (int.TryParse(str, out int result))
            {
                FindCategory(result);
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
        /// <param name="str">Переменная, для ответа пользователя.</param>
        public void SetCurrentCategory(string str)
        {
            if (int.TryParse(str, out int categoryId))
            {
                var category = GetCategories().FirstOrDefault(category => category.Id == categoryId);
                if (category != null)
                {
                    CurrentCategory = category;
                }
            }
        } 
    }
}
