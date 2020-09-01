using System;
using System.Collections.Generic;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика рецептов.
    /// </summary>
    public class RecipesControler
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
        public RecipesControler(IRecipeUnityOfWork recipeUnityOfWork)
        {
            _recipeUnityOfWork = recipeUnityOfWork;
        }
        /// <summary>
        /// Загрузка списка рецепта в приложение.
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
            _recipeUnityOfWork.Save((UnitOfWork)_recipeUnityOfWork);
        }
        /// <summary>
        /// Добавить рецепт.
        /// </summary>
        /// <param name="nameRecipe">Название рецепта.</param>
        /// <param name="category">Категория рецепта.</param>
        /// <param name="subcategories">Подкатегория рецепта.</param>
        /// <param name="description">Описание.</param>
        /// <param name="ingredients">Ингредиенты.</param>
        /// <param name="countIngred">Количество ингредиентов.</param>
        /// <param name="recipes">Пошаговая инструкция.</param>
        public void AddRecipe(string nameRecipe,string category, string subcategories, string description, List<string>ingredients, List<string> countIngred, List<string>recipes)
        {
            var getRecipes = _recipeUnityOfWork.RecipesRepository.Get();
            foreach (var recip in getRecipes)
            {
                if(recip.Name==nameRecipe)
                {
                    Console.WriteLine("Такой рецепт уже существует.");
                }
            }
            
            Recipe r = new Recipe(nameRecipe,category ,subcategories, description, ingredients, countIngred, recipes);
            _recipeUnityOfWork.RecipesRepository.Insert(r ?? throw new ArgumentNullException("Нельзя добавить пустой рецепт.",nameof(recipes)));
            CurrentRecipes = r;
        }
        /// <summary>
        /// Поиск рецепта.
        /// </summary>
        /// <param name="nameRecipes">Название рецепта.</param>
        /// <return>Истина, есть ли такой рецепт.</return>
        private bool FindRecipe(string nameRecipes)
        {
            for(int recipe=0; recipe< _recipeUnityOfWork.RecipesRepository.Get().Count;recipe++)
            {
                if(_recipeUnityOfWork.RecipesRepository.Get()[recipe].Name.ToLower() ==nameRecipes.ToLower())
                {
                    CurrentRecipes = _recipeUnityOfWork.RecipesRepository.Get()[recipe];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Открытие рецепта.
        /// </summary>
        /// <param name="categoryControler">Контролер категориями.</param>
        /// <param name="ingredientControler">Контролер ингредиентами.</param>
        public void OpenRecipe(List<Category> categories, ref Category currentCategory, string str="") //TODO работают ли комменты
        {
            do
            {
                Console.Clear();
                Console.Write("Введите название рецeпта : ");
                str = Console.ReadLine();
                if (str.ToLower() == "bye" || str.ToLower() == "back") return;
            } while (!FindRecipe(str));

            foreach (var category in categories)
            {
                if (category.Name == CurrentRecipes.Category)
                    currentCategory = category;        // Устанавливаем активную категорию
            }

            if (!string.IsNullOrWhiteSpace(CurrentRecipes.Name))
            {
                foreach (var subcategory in currentCategory.Subcategories)
                {
                    if (subcategory == CurrentRecipes.Subcategory)
                        currentCategory.CurrentSubcategories = subcategory;        // Устанавливаем активную подкатегорию
                }
                DisplayCurrentRicepe();         // Показывает рецепт
            }
        }
        /// <summary>
        /// Метод дя отображения выбранного рецепта.
        /// </summary>
        public void DisplayCurrentRicepe()
        {
            Console.Clear();
            Console.WriteLine("Название : " + CurrentRecipes.Name);
            Console.WriteLine(CurrentRecipes.Description);

            Console.WriteLine("Ингридиенты : ");
            for (int ingredient = 0; ingredient < CurrentRecipes.Ingredients.Count; ingredient++)
            {
                Console.WriteLine((ingredient + 1).ToString() + $". {CurrentRecipes.Ingredients[ingredient]} - {CurrentRecipes.CountIngredients[ingredient]} ");
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
        /// <param name="categoryControler">Контролер категориями.</param>
        public bool WalkRecipes(Category category, string str="")
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                var listRecipes = new List<Recipe>(); // Список рецептов активной подкатегории
                for (int recipe = 0, count = 1; recipe < GetRecipes().Count; recipe++)
                {
                    if (GetRecipes()[recipe].Subcategory == category.CurrentSubcategories
                       & GetRecipes()[recipe].Category == category.Name)
                    {
                        Console.WriteLine($"{count++}. {GetRecipes()[recipe].Name}");
                        listRecipes.Add(GetRecipes()[recipe]);
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
