using System;

namespace Task3.BL.Model
{
    /// <summary>
    /// Модель таблицы IngredientsInRecipe, что содержит в себе ингредиент и его колличество для рецепта
    /// </summary>
    [Serializable]
    public class IngredientsInRecipe
    {
        public int Id { get; set; }
        public string Ingredient { get; set; }
        public string CountIngredients { get; set; }
    }
}
