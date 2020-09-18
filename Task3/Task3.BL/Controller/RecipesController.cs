using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Interfaces;
using Task2.BL.Model;
using Task3.BL.BD;
using Task3.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика рецептов.
    /// </summary>
    public class RecipesController
    {
        /// <summary>
        /// Репозиторий рецептов.
        /// </summary>
        private IRecipeUnityOfWork _recipeUnityOfWork;
        /// <summary>
        /// Активный рецепт.
        /// </summary>
        public Recipe CurrentRecipe { get; set; }
        /// <summary>
        /// Создает контролер моделью рецепта.
        /// </summary>
        public RecipesController(IRecipeUnityOfWork recipeUnityOfWork)
        {
            _recipeUnityOfWork = recipeUnityOfWork;
        }
        /// <summary>
        /// Загрузка списка рецепта.
        /// </summary>
        /// <returns>Список рецептов.</returns>
        public List<Recipe> GetRecipes()
        {
            return _recipeUnityOfWork.RecipesRepository.Get();
        }
        /// <summary>
        /// Сохранение рецепта.
        /// </summary>
        public void Save()
        {
            _recipeUnityOfWork.Save();
        }
        /// <summary>
        /// Добавить рецепт.
        /// </summary>
        /// <param name="nameRecipe">Название рецепта.</param>
        /// <param name="subcategoriesId">Индекс подкатегории рецепта.</param>
        /// <param name="description">Описание.</param>
        /// <param name="ingredientsId">Индекс ингредиентов.</param>
        /// <param name="countIngred">Количество ингредиентов.</param>
        /// <param name="stepsHowCooking">Пошаговая инструкция.</param>
        public void AddRecipe(string nameRecipe, int subcategoriesId, string description, List<int> ingredientsId, List<string> countIngred, List<string> stepsHowCooking)
        {
            var getRecipes = _recipeUnityOfWork.RecipesRepository.Get();
            foreach (var recipe in getRecipes)
            {
                if(recipe.Name==nameRecipe)
                {
                    Console.WriteLine("Такой рецепт уже существует.");
                    CurrentRecipe = recipe;
                    return;
                }
            }
            
            Recipe r = new Recipe(getRecipes.Last().Id+1,nameRecipe,subcategoriesId, description, ingredientsId, countIngred, stepsHowCooking);
            _recipeUnityOfWork.RecipesRepository.Insert(r);
            CurrentRecipe = r;
        }
        /// <summary>
        /// Поиск рецепта.
        /// </summary>
        /// <param name="recipesId">Индекс рецепта.</param>
        /// <return>Рецепт.</return>
        public Recipe FindRecipe(int recipesId)
        {
            return GetRecipes().First(r=>r.Id == recipesId);
        }
        /// <summary>
        /// Открытие рецепта.
        /// </summary>
        public void OpenRecipe()
        {
            Console.Clear();
            
            foreach (var recipes in GetRecipes())
            {
                Console.WriteLine($"{recipes.Id}. {recipes.Name}.");
            }

            Console.Write("Введите id рецeпта : ");
            var str = Console.ReadLine();
            if (str.ToLower() == "bye" || str.ToLower() == "back") return;
            if (!int.TryParse(str, out int result)) return;

            CurrentRecipe = FindRecipe(result);

            if (!string.IsNullOrWhiteSpace(CurrentRecipe.Name))
            {
                DisplayCurrentRicepe();         // Показывает рецепт
            }
        }
        /// <summary>
        /// Метод дя отображения выбранного рецепта.
        /// </summary>
        public void DisplayCurrentRicepe()
        {
            Console.Clear();
            
            Console.WriteLine("Название : " + CurrentRecipe.Name);
            Console.WriteLine("Описание :"+CurrentRecipe.Description);

            Console.WriteLine("Ингридиенты : ");

            var ingr = CurrentRecipe.Ingredients;

            var ingredients = SQLScriptManager.SQLQuerry<IngredientsInRecipe>(
            $"SELECT {ingr}.Id, Ingredients.Name as Ingredient, {ingr}.CountIngredients FROM Ingredients " +
            $"INNER JOIN {ingr} ON " +
            $"{ingr}.IngredientsId = Ingredients.Id " +
            $"INNER JOIN REcipes ON Recipes.Ingredients = \'{ingr}\'");

            foreach (var ingradient in ingredients)
            {
                Console.WriteLine(ingradient.Id + ". " + ingradient.Ingredient + " - " + ingradient.CountIngredients);
            }

            Console.WriteLine("Шаги приготовления : ");

            var stepsRefer = CurrentRecipe.StepsHowCooking;
            var steps = SQLScriptManager.SQLQuerry<StepsInRecipe>(
            $"SELECT {stepsRefer}.Id, {stepsRefer}.Description FROM {stepsRefer} "  +
            $"INNER JOIN Recipes ON Recipes.StepsHowCooking= \'{stepsRefer}\'");

            foreach (var step in steps)
            {
                Console.WriteLine(step.Id + ". " + step.Description);
            }

            Console.WriteLine("\n\t\t*enter*");
            Console.ReadLine();
        }
        /// <summary>
        /// Метод для выбора пользователем конкретного рецепта из списка.
        /// </summary>
        /// <param name="subcategoryId">Индекс активной подкатегории.</param>
        /// <param name="str">Переменная для обработки ответа пользователя.</param>
        public bool WalkRecipes(int subcategoryId, string str = "")
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                var getRecipes = GetRecipes();
                var listRecipes = new List<Recipe>(); // Список рецептов активной подкатегории
                for (int recipe = 0, count = 1; recipe < getRecipes.Count; recipe++)
                {
                    if (getRecipes[recipe].SubcategoryId == subcategoryId)
                    {
                        Console.WriteLine($"{count++}. {getRecipes[recipe].Name}");
                        listRecipes.Add(getRecipes[recipe]);
                    }
                }

                Console.WriteLine("Рецепт (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    CurrentRecipe = listRecipes[result - 1];
                    return false;
                }
                else
                {
                    if (ConsoleManager.IsExit(str))
                    {
                        return true;
                    }
                }
                Console.WriteLine("\tSome mistake, try again...");
            }
        }
    }
}
