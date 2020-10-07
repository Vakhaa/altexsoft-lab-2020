using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    /// <summary>
    /// Рецепт.
    /// </summary>
    public class Recipe : BaseEntity
    {
        #region Свойства
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Подкатегория.
        /// </summary>
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ссылка на таблицу, что содержит в себе список ингредиентов для рецпта и их колличество 
        /// </summary>
        public List<IngredientsInRecipe> Ingredients { get; } 
        /// <summary>
        /// Ссылка на таблицу где описаны шаги приготовления.
        /// </summary>
        public List<StepsInRecipe> StepsHowCooking { get;}
        #endregion
        public Recipe() { }
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Название рецепта.</param>
        /// <param name="categoryId"> Id категории.</param>
        /// <param name="description">Описание.</param>
        public Recipe(string name, int categoryId, string description)
        {
            #region  Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Название рецепта не может быть пустым");
            }
            if (categoryId == 0)
            {
                throw new ArgumentNullException(nameof(categoryId), "Должна быть указана подкатегория");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), "Описание не может быть пустым");
            }
            #endregion

            Name = name;
            CategoryId = categoryId;
            Description = description;
            /*Ingredients = "IngredientsInRecipe_" + Id;
            if (SQLScriptManager.IsExists<IngredientsInRecipe>(Ingredients))
            {
                throw new Exception("IngredientsInRecipe_" + Id + " уже существует");
            }
            else
            {
                SQLScriptManager.CreateTabelIngredirntsForRecipes(Id);
                for (int i = 0; i < ingredientsId.Count; i++)
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
            }*/
        }
    }
}
