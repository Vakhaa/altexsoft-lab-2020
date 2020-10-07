using System;
using System.Collections.Generic;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Ингредиент.
    /// </summary>
    [Serializable]
    public class Ingredient : BaseEntity
    {
        /// <summary>
        /// Название ингредиента.
        /// </summary>
        public string Name { get; set; }
        public List<IngredientsInRecipe> IngredientsInRecipe { get;}
        public Ingredient() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название ингредиента.</param>
        public Ingredient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Имя ингредиента не должно быть пустым.");
            }
            Name = name;
        }
    }
}
