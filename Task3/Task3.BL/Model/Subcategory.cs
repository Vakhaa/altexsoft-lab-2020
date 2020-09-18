using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Подкатегории.
    /// </summary>
    [Serializable]
    public class Subcategory
    {
        public int Id { get; set; }
        /// <summary>
        /// Название подкатегории.
        /// </summary>
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Subcategory() {}
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название категории.</param>
        public Subcategory( int id, string name, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Должно быть имя подкатегории", nameof(name));
            }
            Name = name;
            CategoryId = categoryId;
            Id = id;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
