using System.Collections.Generic;
using System.Linq;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    /// <summary>
    /// Логика категорий.
    /// </summary>
    public class CategoryController
    {
        /// <summary>
        /// Репозиторий категории.
        /// </summary>
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// Активная категория.
        /// </summary>
        public Category CurrentCategories { get; set; }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="categoryRepository">Репозиторий категории.</param>
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
            return _unitOfWork.Repository.ListAsync<Category>().Result.Where(c => c.ParentId == null).ToList();
        }
        /// <summary>
        /// Сохранение категорий.
        /// </summary>
        public void Save()
        {
            _unitOfWork.SaveChangesAsync();
        }
        public void AddCategory(string nameCategory)
        {
            _unitOfWork.Repository.AddAsync(new Category(nameCategory)); //parentid
        }
        /// <summary>
        /// Поиск категории.
        /// </summary>
        /// <param name="idCategory">Id категории.</param>
        public void FindCategory(int idCategory)
        {
            CurrentCategories = GetCategories().First(c => c.Id == idCategory);
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        public bool WalkCategories(string str = "")
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
        /// <param name="isExist">Аргумент для выхода с цыкла.</param>
        public void SetCurrentCategory(string str="",bool isExist = false)
        {
            while (true)
            {
                if (int.TryParse(str, out int categoryId))
                {
                    var category = GetCategories().FirstOrDefault(category => category.Id == categoryId);
                    if (category != null)
                    {
                        CurrentCategories = category;
                        isExist = true;
                    }
                }
                if (isExist)
                {
                    break;
                }
            }
        } 
    }
}
