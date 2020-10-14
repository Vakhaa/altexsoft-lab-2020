using System;
using System.Collections.Generic;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public List<IngredientsInRecipe> IngredientsInRecipe { get;}
        public Ingredient() { }
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
