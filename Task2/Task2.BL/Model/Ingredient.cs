using System;

namespace Task2.BL.Model
{
    /// <summary>
    /// Ингредиент.
    /// </summary>
    [Serializable]
    public class Ingredient
    {
        /// <summary>
        /// Название ингредиента.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название ингредиента.</param>
        public Ingredient(string Name)
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Имя ингредиента не должно быть пустым.", nameof(Name));
            }
            this.Name = Name;
        }
        public override string ToString()
        {
            return "Ingredient";
        }
    }
}
