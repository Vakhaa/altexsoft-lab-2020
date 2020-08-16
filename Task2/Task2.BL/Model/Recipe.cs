using System;
using System.Collections.Generic;

namespace Task2.BL.Model
{
    //TODO Change List<string> Ingradients => Class Ingradients
    /// <summary>
    /// Рецепт
    /// </summary>
    [Serializable]
    public class Recipe
    {
        #region Свойства
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Ингредиенты
        /// </summary>
        public List<string> Ingredients { get; }
        /// <summary>
        /// Пошаговый рецепт
        /// </summary>
        public List<string> StepsHowCooking { get; }
        #endregion
        /// <summary>
        /// Создать новый рецепт.
        /// </summary>
        /// <param name="Name">Имя.</param>
        /// <param name="Ingredients">ингредиенты.</param>
        /// <param name="StepsHowCooking">Рецепт.</param>
        public Recipe(string Name, string Category,string Description, List<string> Ingredients, List<string> StepsHowCooking)
        {
            #region  Проверка условий
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Название рецепта не может быть пустым", nameof(Name));
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                throw new ArgumentNullException("Название категории не может быть пустым", nameof(Category));
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new ArgumentNullException("Описание не может быть пустым", nameof(Description));
            }
            foreach (var ingredient  in Ingredients)
            {
                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    throw new ArgumentNullException("Название ингредиента не может быть пустым", nameof(Ingredients));
                }
            }
            foreach (var steps in StepsHowCooking)
            {
                if (string.IsNullOrWhiteSpace(steps))
                {
                    throw new ArgumentNullException("Должен быть рецепта блюда", nameof(StepsHowCooking));
                }
            }
            #endregion

            this.Name = Name;
            this.Category = Category;
            this.Description = Description;
            this.Ingredients = Ingredients;
            this.StepsHowCooking = StepsHowCooking;
        }
        public override string ToString()
        {
            return "Recipes";
        }
    }
}