using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Interfaces;
using Task2.BL.Model;

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
        public Recipe CurrentRecipes { get; set; }
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
        /// <param name="categoryId">Индекс категории рецепта.</param>
        /// <param name="subcategoriesId">Индекс подкатегории рецепта.</param>
        /// <param name="description">Описание.</param>
        /// <param name="ingredientsId">Индекс ингредиентов.</param>
        /// <param name="countIngred">Количество ингредиентов.</param>
        /// <param name="recipes">Пошаговая инструкция.</param>
        public void AddRecipe(string nameRecipe,int categoryId, int subcategoriesId, string description, List<int> ingredientsId, List<string> countIngred, List<string>recipes)
        {
            var getRecipes = _recipeUnityOfWork.RecipesRepository.Get();
            foreach (var recipe in getRecipes)
            {
                if(recipe.Name==nameRecipe)
                {
                    Console.WriteLine("Такой рецепт уже существует.");
                }
            }
            
            Recipe r = new Recipe(nameRecipe, categoryId ,subcategoriesId, description, ingredientsId, countIngred, recipes);
            _recipeUnityOfWork.RecipesRepository.Insert(r);
            CurrentRecipes = r;
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
        /// <param name="ingredients">Список ингредиентов.</param>
        public void OpenRecipe(List<Ingredient> ingredients)
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

            CurrentRecipes = FindRecipe(result);


            if (!string.IsNullOrWhiteSpace(CurrentRecipes.Name))
            {
                DisplayCurrentRicepe(ingredients);         // Показывает рецепт
            }
        }
        /// <summary>
        /// Метод дя отображения выбранного рецепта.
        /// </summary>
        /// <param name="ingredients">Список ингредиентов.</param>
        public void DisplayCurrentRicepe(List<Ingredient> ingredients)
        {
            Console.Clear();
            Console.WriteLine("Название : " + CurrentRecipes.Name);
            Console.WriteLine(CurrentRecipes.Description);

            Console.WriteLine("Ингридиенты : ");
            var currentIngredients = CurrentRecipes.IngredientsId;
            for (int id = 1; id <= currentIngredients.Count; id++)
            {
                Console.WriteLine((id).ToString() + 
                    $". {ingredients.First(i=>i.Id == currentIngredients[id-1]).Name} - {CurrentRecipes.CountIngredients[id-1]} ");
            }

            Console.WriteLine("Шаги приготовления : ");
            for (int step = 0; step < CurrentRecipes.StepsHowCooking.Count; step++)
            {
                Console.WriteLine((step + 1).ToString() + $". {CurrentRecipes.StepsHowCooking[step]}");
            }
            Console.WriteLine("\n\t\t*enter*");
            Console.ReadLine();
        }
        /// <summary>
        /// Метод для выбора пользователем конкретного рецепта из списка.
        /// </summary>
        /// <param name="categoryId">Индекс активной категории.</param>
        /// <param name="subcategoryId">Индекс активной подкатегории.</param>
        /// <param name="str">Переменная для обработки ответа пользователя.</param>
        public bool WalkRecipes(int categoryId, int subcategoryId, string str = "")
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                var getRecipes = GetRecipes();
                var listRecipes = new List<Recipe>(); // Список рецептов активной подкатегории
                for (int recipe = 0, count = 1; recipe < getRecipes.Count; recipe++)
                {
                    if (getRecipes[recipe].SubcategoryId == subcategoryId
                       && getRecipes[recipe].CategoryId == categoryId)
                    {
                        Console.WriteLine($"{count++}. {getRecipes[recipe].Name}");
                        listRecipes.Add(getRecipes[recipe]);
                    }
                }

                Console.WriteLine("Рецепт (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    CurrentRecipes = listRecipes[result - 1];
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
