using System;
using System.Collections.Generic;
using System.Linq;

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
        public Recipe(string name,string category, string subcategory,string description, List<string> ingredients,List<string> countIngredients, List<string> stepsHowCooking)
        {
            #region  Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Название рецепта не может быть пустым", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentNullException("Название категории не может быть пустым", nameof(category));
            }
            if (string.IsNullOrWhiteSpace(subcategory))
            {
                throw new ArgumentNullException("Название подкатегории не может быть пустым", nameof(subcategory));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("Описание не может быть пустым", nameof(description));
            }
            if (!ingredients.All(i=>i.Length>0))
            {
                throw new ArgumentNullException("Название ингредиента не может быть пустым", nameof(ingredients));
            }
            if (!countIngredients.All(ci => ci.Length > 0))
            {
                throw new ArgumentNullException("Колличество ингредиентов не может быть пустыми", nameof(countIngredients));
            }
            if (!stepsHowCooking.All(s => s.Length > 0))
            {
                throw new ArgumentNullException("Должен быть пошаговый рецепта блюда.", nameof(stepsHowCooking));
            }
            #endregion

            Name = name;
            Category = category;
            Subcategory = subcategory;
            Description = description;
            Ingredients = ingredients;
            CountIngredients = countIngredients;
            StepsHowCooking = stepsHowCooking;
        }
        public override string ToString()
        {
            return "Recipes";
        }
    }
}