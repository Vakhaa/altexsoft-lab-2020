using System;

namespace Task3.BL.Model
{
    /// <summary>
    /// Модель таблицы StepsInRecipe, что содержит в себе шаги приготовления рецепта
    /// </summary>
    [Serializable]
    public class StepsInRecipe
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
