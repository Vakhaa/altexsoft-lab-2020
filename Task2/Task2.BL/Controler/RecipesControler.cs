using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика рецептов
    /// </summary>
    public class RecipesControler
    {
        /// <summary>
        /// Рецепты
        /// </summary>
        public List<Recipe> Recipes{get;}
        /// <summary>
        /// Активный рецепт
        /// </summary>
        public Recipe CurrentRecipes { get; set; }
        /// <summary>
        /// Создает контролер моделью рецепта
        /// </summary>
        public RecipesControler()
        {
            Recipes=GetRecipes();
        }
        /// <summary>
        /// Загрузка списка рецепта в приложение
        /// </summary>
        /// <returns>Рецепт</returns>
        private List<Recipe> GetRecipes()
        {
            return JSONReader.DeserialezeFile<Recipe>(Recipes,"rcps.json");
        }
        /// <summary>
        /// Сохранение рецепта
        /// </summary>
        public void Save()
        {
            JSONReader.Save(Recipes, "rcps.json");
        }
        /// <summary>
        /// Добавить рецепт
        /// </summary>
        /// <param name="recipes">Рецепт</param>
        public void AddRecipes(string NameRecipe, string categories, string description, List<string>ingradients,List<string>recipes)
        {
            foreach(var recip in Recipes)
            {
                if(recip.Name==NameRecipe)
                {
                    Console.WriteLine("Такой рецепт уже существует");
                }
            }
            
            Recipe r = new Recipe(NameRecipe, categories, description, ingradients, recipes);
            Recipes.Add(r ?? throw new ArgumentNullException("Нельзя добавить пустой рецепт",nameof(recipes)));
            CurrentRecipes = r; //точно ли нужно делать рецепт активным, возможно он уже будет активным на тот момент так или иначе
        }
        /// <summary>
        /// Поиск рецепта
        /// </summary>
        /// <param name="NameRecipes">Название рецепта</param>
        public void FindRecipes(string NameRecipes)
        {
            CurrentRecipes = Recipes.SingleOrDefault(r=>r.Name==NameRecipes);
        }
    }
}
