using System;
using System.Collections.Generic;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика категорий.
    /// </summary>
    public class CategoryControler
    {
        /// <summary>
        /// Репозиторий категории.
        /// </summary>
        private ICategoryRepository _categoryReposyitory;
        /// <summary>
        /// Активная категория.
        /// </summary>
        public Category CurrentCategories { get; set; }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="categoryRepository">Репозиторий категории.</param>
        public CategoryControler(ICategoryRepository categoryRepository)
        {
            _categoryReposyitory = categoryRepository;
        }
        /// <summary>
        /// Загрузка списка категорий в приложение.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public List<Category> GetCategories()
        {
            return _categoryReposyitory.CategoryRepository.Get();
        }
        /// <summary>
        /// Сохранение категорий.
        /// </summary>
        public void Save()
        {
            _categoryReposyitory.Save((UnitOfWork)_categoryReposyitory);
        }

        /*/// <summary>
        /// Добавления новой категории.
        /// </summary>
        /// <param name="nameCategory">Название категории.</param>
        public void AddCategory(string nameCategory)
        {
            foreach (var category in Categories)
            {
                if (category.Name == nameCategory) Console.WriteLine("Такая категория уже существует.");
            }

            Category c = Category.NewCategory(nameCategory);
            Categories.Add(c ?? throw new ArgumentNullException("Нельзя добавить пустую категорию.", nameof(nameCategory)));
            CurrentCategoties = c;
        }*/

        /// <summary>
        /// Поиск категории.
        /// </summary>
        /// <param name="idCategory">Id категории.</param>
        public void FindCategory(int idCategory)
        {
            CurrentCategories = _categoryReposyitory.CategoryRepository.GetByID(idCategory); 
                //Categories.SingleOrDefault(c => c.Name == nameCategory);
        }
        /// <summary>
        /// Метод для отображения категорий.
        /// </summary>
        public void DisplayCategory()
        {
            int count = 1;
            foreach (var category in _categoryReposyitory.CategoryRepository.Get())
            {
                Console.WriteLine(count.ToString() + ". " + category.Name);
                count++;
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        public bool WalkCategories()
        {
            string str;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplayCategory();
                Console.WriteLine("Категория (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    FindCategory(result - 1);
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
        public void SetCurrentCategory()
        {
            bool _isExist = false;
            int categories;
            while (true)
            {
                Console.Clear();
                DisplayCategory();
                Console.Write("Ввидите категорию блюда (id) : ");
                if (int.TryParse(Console.ReadLine(), out categories))
                    foreach (var category in GetCategories())
                    {
                        if (category.Name == GetCategories()[categories - 1].Name)
                        {
                            CurrentCategories = category;
                            _isExist = true;
                        }
                    }
                if (_isExist)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Метод для отображения подкатегорий
        /// </summary>
        public void DisplaySubcategory()
        {
            for (int subcategory = 0; subcategory < CurrentCategories.Subcategories.Count; subcategory++)
            {
                Console.WriteLine($"{subcategory + 1}. {CurrentCategories.Subcategories[subcategory]}");
            }
        }

        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        public void AddSubcategoris()
        {
            Console.Clear();
            SetCurrentCategory();
            Console.Clear();
            DisplaySubcategory();
            Console.WriteLine("Ввидите подкатегорию блюда (названия): ");
            var str = Console.ReadLine();
            if (!CurrentCategories.FindSubcategory(str))
            {
                CurrentCategories.AddSubcategories(str);
                Save();
            }
            else
            {
                Console.WriteLine("Такая подкатегория уже есть.");
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        public bool WalkSubcategories()
        {
            string str;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplaySubcategory();
                Console.WriteLine("Подкатегория (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    CurrentCategories.CurrentSubcategories = CurrentCategories.Subcategories[result - 1];
                    return false;
                }
                else
                {
                    if (ConsoleManager.IsExit(str))
                    {
                        return true;
                    }
                }
            }
        }
    }
}
