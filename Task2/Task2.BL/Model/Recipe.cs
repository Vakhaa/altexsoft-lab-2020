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
        private static int _lastId = 0;
        #region Свойства
        public int Id { get; }
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Kатегория.
        /// </summary>
        public int CategoryId { get; }
        /// <summary>
        /// Подкатегория.
        /// </summary>
        public int SubcategoryId { get; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Ингредиенты.
        /// </summary>
        public List<int> IngredientsId { get; }
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
        public Recipe(string name, int categoryId, int subcategoryId,string description, List<int> ingredientsId,List<string> countIngredients, List<string> stepsHowCooking)
        {
            #region  Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Название рецепта не может быть пустым", nameof(name));
            }
            if (categoryId==0)
            {
                throw new ArgumentNullException("Должна быть категория.", nameof(categoryId));
            }
            if (subcategoryId==0)
            {
                throw new ArgumentNullException("Должна быть указана подкатегория", nameof(subcategoryId));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("Описание не может быть пустым", nameof(description));
            }
            if (ingredientsId.Count==0)
            {
                throw new ArgumentNullException("Блюдо должно иметь ингредиенты.", nameof(ingredientsId));
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
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            Description = description;
            IngredientsId = ingredientsId;
            CountIngredients = countIngredients;
            StepsHowCooking = stepsHowCooking;
            Id = ++_lastId;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}