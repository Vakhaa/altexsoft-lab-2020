using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Task2.BL.Model
{
    /// <summary>
    /// Категории
    /// </summary>
    [Serializable]
    public class Category
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Список подкатегорий
        /// </summary>
        public List<string> Subcategories { get; set;  }
        /// <summary>
        /// Выбраная категория
        /// </summary>
        public string CurrentSubcategories { get; set; }
        /// <summary>
        /// Создание категории
        /// </summary>
        /// <param name="NameCategory">Имя категории</param>
        public Category(string NameCategory)
        {
            if(string.IsNullOrWhiteSpace(NameCategory))
            {
                throw new ArgumentNullException("Должно быть имя категории", nameof(NameCategory));
            }
            Name = NameCategory;
            Console.WriteLine("Ввидите подкатегорию: ");
            AddSubcategories(Console.ReadLine());
        }
        /// <summary>
        /// Создание подкатегории
        /// </summary>
        /// <param name="NameSubcategories">Название подкатегории</param>
        public void AddSubcategories(string NameSubcategories)
        {
            if (string.IsNullOrWhiteSpace(NameSubcategories))
            {
                throw new ArgumentNullException("Должно быть имя подкатегории", nameof(NameSubcategories));
            }
            foreach(var subcategory in Subcategories)
            {
                if (subcategory == NameSubcategories)
                {
                    Console.WriteLine("Такая категория уже есть.");
                    return;
                }
            }
            Subcategories.Add(NameSubcategories);
        }

        public override string ToString()
        {
            return "Category";
        }
    }
}
