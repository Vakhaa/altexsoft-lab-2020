using System;
using System.Collections.Generic;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class Recipe : BaseEntity
    {
        #region Свойства
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Подкатегория.
        /// </summary>
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        /// <summary>
        /// Ссылка на таблицу, что содержит в себе список ингредиентов для рецпта и их колличество 
        /// </summary>
        public IEnumerable<IngredientsInRecipe> IngredientsInRecipe { get; set; } 
        /// <summary>
        /// Ссылка на таблицу где описаны шаги приготовления.
        /// </summary>
        public IEnumerable<StepsInRecipe> StepsHowCooking { get; set; }
        #endregion
        public Recipe() { }
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
        }
    }
}
