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
        private static int _lastId=0;
        public int Id { get; }
        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Список подкатегорий.
        /// </summary>
        public List<int> SubcategoriesId { get; set; }    
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название категории.</param>
        /// <param name="Subcategories">Список подкатегорий.</param>
        public Category(int id, string name, List<int> subcategoriesId)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Должно быть имя категории", nameof(name));
            }
            Name = name;
            if(subcategoriesId==null)
            {
                SubcategoriesId = new List<int>();
            }
            else
            {
                SubcategoriesId = new List<int>();
                foreach (var subcategory in subcategoriesId)
                {
                    this.SubcategoriesId.Add(subcategory);
                }
            }
            _lastId++;
            if (Id == 0)
            {
                Id = _lastId;
            }
            else
            {
                Id = id; 
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}