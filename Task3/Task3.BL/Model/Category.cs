using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Категории.
    /// </summary>
    [Serializable]
    public class Category
    {
        public int Id { get; set; }
        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; }
        public Category() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название категории.</param>
        /// <param name="Subcategories">Список подкатегорий.</param>
        public Category(int id, string name )
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Должно быть имя категории", nameof(name));
            }
            Name = name;
            Id = id;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}