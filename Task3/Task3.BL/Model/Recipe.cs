using System;
using System.Collections.Generic;
using System.Linq;
using Task3.BL.BD;
using Task3.BL.Model;

namespace Task2.BL.Model
{
    /// <summary>
    /// Рецепт.
    /// </summary>
    [Serializable]
    public class Recipe
    {
        #region Свойства
        public int Id { get; set; }
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Подкатегория.
        /// </summary>
        public int SubcategoryId { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ссылка на таблицу, что содержит в себе список ингредиентов для рецпта и их колличество 
        /// </summary>
        public string Ingredients { get; set; }
        /// <summary>
        /// Ссылка на таблицу где описаны шаги приготовления.
        /// </summary>
        public string StepsHowCooking { get; set; }
        #endregion
        public Recipe() { }
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
        public Recipe(int id, string name, int subcategoryId, string description,List<int> ingredientsId, List<string> countIngredients , List<string> stepsHowCooking)
        {
            #region  Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Название рецепта не может быть пустым", nameof(name));
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
            SubcategoryId = subcategoryId;
            Description = description;
            Id = id;
            Ingredients = "IngredientsInRecipe_" + Id;
            if (SQLScriptManager.IsExists<IngredientsInRecipe>(Ingredients))
            {
                throw new Exception("IngredientsInRecipe_" + Id+" уже существует");
            }
            else
            {
                SQLScriptManager.CreateTabelIngredirntsForRecipes(Id);
                for (int i =0; i<ingredientsId.Count;i++)
                {
                    SQLScriptManager.SQLQuerry($"INSERT INTO {Ingredients} VALUES(" +
                    $"{ingredientsId[i]}," +
                    $"N\'{countIngredients[i]}\')");
                }
            }
            StepsHowCooking = "StepsInRecipe_" + Id;
            if (SQLScriptManager.IsExists<StepsInRecipe>(StepsHowCooking))
            {
                throw new Exception("StepsInRecipe_" + Id + " уже существует");
            }
            else
            {
                SQLScriptManager.CreateTabelStepsForRecipes(Id);
                for (int i = 0; i < ingredientsId.Count; i++)
                {
                    SQLScriptManager.SQLQuerry($"INSERT INTO {StepsHowCooking} VALUES(" +
                    $"N\'{stepsHowCooking[i]}\')");
                }
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}