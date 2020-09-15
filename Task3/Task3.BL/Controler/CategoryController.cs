using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика категорий.
    /// </summary>
    public class CategoryController
    {
        /// <summary>
        /// Репозиторий категории.
        /// </summary>
        private ICategoryUnityOfWork _categoryUnityOfWork;
        /// <summary>
        /// Активная категория.
        /// </summary>
        public Category CurrentCategories { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="categoryRepository">Репозиторий категории.</param>
        public CategoryController(ICategoryUnityOfWork categoryUnityOfWork)
        {
            _categoryUnityOfWork = categoryUnityOfWork;
        }
        /// <summary>
        /// Загрузка списка категорий в приложение.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public List<Category> GetCategories()
        {
            return _categoryUnityOfWork.CategoryRepository.Get();
        }
        /// <summary>
        /// Сохранение категорий.
        /// </summary>
        public void Save()
        {
            _categoryUnityOfWork.Save();
        }
 
        /*public void Add()
        {
            _categoryUnityOfWork.CategoryRepository.Insert(new Category(0,Console.ReadLine(), new List<int>()));
        }*/

        /// <summary>
        /// Поиск категории.
        /// </summary>
        /// <param name="idCategory">Id категории.</param>
        public void FindCategory(int idCategory)
        {
            CurrentCategories = GetCategories().First(c=>c.Id== idCategory);
        }
        /// <summary>
        /// Метод для отображения категорий.
        /// </summary>
        public void DisplayCategory()
        { 
            foreach (var category in _categoryUnityOfWork.CategoryRepository.Get())
            {
                Console.WriteLine(category.Id.ToString()+ ". " + category.Name);
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        public bool WalkCategories(string str="")
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplayCategory();
                Console.WriteLine("Категория (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    FindCategory(result);
                    return false;
                }
                else
                {
                    if (ConsoleManager.IsExit(str))
                    {
                        return true;
                    }
                }
                Console.WriteLine("\tSome mistake, try again...");
            }
        }
        /// <summary>
        /// Установка конкретной категории.
        /// </summary>
        /// <param name="isExist">Аргумент для выхода с цыкла.</param>
        public void SetCurrentCategory(bool isExist = false)
        {
            while (true)
            {   
                Console.Clear();
                DisplayCategory();
                Console.Write("Ввидите категорию блюда (id) : ");
                if (int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    var category = GetCategories().FirstOrDefault(category => category.Id == categoryId);
                    if (category!=null)
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
