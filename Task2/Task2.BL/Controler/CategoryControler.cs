using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика категорий
    /// </summary>
    public class CategoryControler
    {
        /// <summary>
        /// Список категорий
        /// </summary>
        public List<Category> Categories { get;}
        /// <summary>
        /// Активная категория
        /// </summary>
        public Category CurrentCategoties { get; set; }
        /// <summary>
        /// Конструткор
        /// </summary>
        public CategoryControler()
        {
            Categories = GetCategories();
        }
        /// <summary>
        /// Загрузка списка категорий в приложение
        /// </summary>
        /// <returns>Категории</returns>
        private List<Category> GetCategories()
        {
            return JSONReader.DeserialezeFile<Category>(Categories, "ctgrs.json");
        }
        /// <summary>
        /// Сохранение категорий
        /// </summary>
        public void Save()
        {
            JSONReader.Save(Categories, "ctgrs.json");
        }
        /// <summary>
        /// Добавления новой категории
        /// </summary>
        /// <param name="NameCategory">Название категории</param>
        public void AddCategory(string NameCategory)
        {
            foreach (var category in Categories)
            {
                if (category.Name == NameCategory) Console.WriteLine("Такая категория уже существует");
            }

            Category c = Category.NewCategory(NameCategory);
            Categories.Add(c ?? throw new ArgumentNullException("Нельзя добавить пустую категорию", nameof(NameCategory)));
            CurrentCategoties = c; //точно ли нужно делать категорию активной, возможно она уже будет активной на тот момент так или иначе
        }
        /// <summary>
        /// Поиск категории
        /// </summary>
        /// <param name="NameCategory">Название категории</param>
        public void FindCategory(string NameCategory)
        {
            CurrentCategoties = Categories.SingleOrDefault(c => c.Name == NameCategory);
        }
    }
}
