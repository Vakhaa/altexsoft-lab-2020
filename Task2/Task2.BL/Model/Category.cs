using System;
using System.Collections.Generic;

namespace Task2.BL.Model
{
    /// <summary>
    /// Категории.
    /// </summary>
    [Serializable]
    public class Category
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Список подкатегорий.
        /// </summary>
        public List<string> Subcategories { get; set;  }
        /// <summary>
        /// Выбраная категория.
        /// </summary>
        public string CurrentSubcategories { get; set; }    
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название категории.</param>
        /// <param name="Subcategories">Список подкатегорий.</param>
        public Category(string Name, List<string> Subcategories)
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Должно быть имя категории", nameof(Name));
            }
            this.Name = Name;
            if(Subcategories==null)
            {
                throw new ArgumentNullException("Должна быть хотя бы одна категория", nameof(Subcategories));
            }
            this.Subcategories = new List<string>();
            foreach (var subcategory in Subcategories)
            {
                this.Subcategories.Add(subcategory);
            }
        }
        /// <summary>
        /// Создания новой категории.
        /// </summary>
        /// <param name="Name">Название категории.</param>
        public static Category NewCategory(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Должно быть имя категории", nameof(Name));
            }
            
            Console.WriteLine("Ввидите подкатегорию: ");
            var NameSubcategory = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(NameSubcategory))
            {
                throw new ArgumentNullException("Должно быть имя категории", nameof(NameSubcategory));
            }
            var Subcategories = new List<string>();
            Subcategories.Add(NameSubcategory);
            return new Category(Name,Subcategories);
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
        /// <summary>
        /// Поиск подкатегории.
        /// </summary>
        /// <param name="nameSubcategory">Названия подкатегории.</param>
        /// <returns>Существует ли подкатегория.</returns>
        public bool FindSubcategory(string nameSubcategory)
        {
            foreach (var subcategory in Subcategories)
            {
                if (subcategory.ToLower() == nameSubcategory.ToLower())
                {
                    CurrentSubcategories = nameSubcategory;
                    return true;
                }
                
            }
            return false;
        }
        public override string ToString()
        {
            return "Category";
        }
    }
}