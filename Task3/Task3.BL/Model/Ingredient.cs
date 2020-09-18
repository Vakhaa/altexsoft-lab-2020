using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Ингредиент.
    /// </summary>
    [Serializable]
    public class Ingredient
    {
        public int Id { get; set; }
        /// <summary>
        /// Название ингредиента.
        /// </summary>
        public string Name { get; set; }
        public Ingredient() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название ингредиента.</param>
        public Ingredient(int id, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя ингредиента не должно быть пустым.", nameof(name));
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
