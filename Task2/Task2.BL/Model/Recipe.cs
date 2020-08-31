using System;
using System.Collections.Generic;

namespace Task2.BL.Model
{
    /// <summary>
    /// Рецепт.
    /// </summary>
    [Serializable]
    public class Recipe
    {
        #region Свойства
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Kатегория.
        /// </summary>
        public string Category { get; }
        /// <summary>
        /// Подкатегория.
        /// </summary>
        public string Subcategory { get; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Ингредиенты.
        /// </summary>
        public List<string> Ingredients { get; }
        /// <summary>
        /// Количество ингредиентов.
        /// </summary>
        public List<string> CountIngredients { get; }
        /// <summary>
        /// Шаги приготовления.
        /// </summary>
        public List<string> StepsHowCooking { get; }
        #endregion
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="Name">Название рецепта.</param>
        /// <param name="Category">Название ктегории.</param>
        /// <param name="Subcategory">Название подкатегории.</param>
        /// <param name="Description">Описание.</param>
        /// <param name="Ingredients">Инргедиенты.</param>
        /// <param name="CountIngredients">Количевство ингредиентов.</param>
        /// <param name="StepsHowCooking">Шаги приготовления</param>
        public Recipe(string Name,string Category, string Subcategory,string Description, List<string> Ingredients,List<string> CountIngredients, List<string> StepsHowCooking)
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
            if (string.IsNullOrWhiteSpace(Subcategory))
            {
                throw new ArgumentNullException("Название подкатегории не может быть пустым", nameof(Subcategory));
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
            foreach (var count in CountIngredients)
            {
                if (string.IsNullOrWhiteSpace(count))
                {
                    throw new ArgumentNullException("Колличество ингредиентов не может быть пустыми", nameof(count));
                }
            }
            foreach (var steps in StepsHowCooking)
            {
                if (string.IsNullOrWhiteSpace(steps))
                {
                    throw new ArgumentNullException("Должен быть рецепта блюда.", nameof(StepsHowCooking));
                }
            }
            #endregion

            this.Name = Name;
            this.Category = Category;
            this.Subcategory = Subcategory;
            this.Description = Description;
            this.Ingredients = Ingredients;
            this.CountIngredients = CountIngredients;
            this.StepsHowCooking = StepsHowCooking;
        }
        public override string ToString()
        {
            return "Recipes";
        }
    }
}