using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Ингредиент.
    /// </summary>
    [Serializable]
    public class Ingredient
    {
        private static int _lastId = 0;
        public int Id { get; set; }
        /// <summary>
        /// Название ингредиента.
        /// </summary>
        public string Name { get; set; }
        public Ingredient() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название ингредиента.</param>
        public Ingredient(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя ингредиента не должно быть пустым.", nameof(name));
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
