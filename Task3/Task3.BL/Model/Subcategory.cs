using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Подкатегории.
    /// </summary>
    [Serializable]
    public class Subcategory
    {
        private static int _lastId = 0;
        public int Id { get; set; }
        /// <summary>
        /// Название подкатегории.
        /// </summary>
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Subcategory() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название категории.</param>
        public Subcategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Должно быть имя подкатегории", nameof(name));
            }
            Name = name;
            Id = ++_lastId;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
