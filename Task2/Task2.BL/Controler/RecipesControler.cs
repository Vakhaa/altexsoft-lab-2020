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
        private IRecipeRepository _recipeRepository;
        /// <summary>
        /// Активный рецепт.
        /// </summary>
        public Recipe CurrentRecipes { get; set; }
        /// <summary>
        /// Создает контролер моделью рецепта.
        /// </summary>
        public RecipesControler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        /// <summary>
        /// Загрузка списка рецепта в приложение.
        /// </summary>
        /// <returns>Список рецептов.</returns>
        public List<Recipe> GetRecipes()
        {
            return _recipeRepository.RecipesRepository.Get();
        }
        /// <summary>
        /// Сохранение рецепта.
        /// </summary>
        public void Save()
        {
            _recipeRepository.Save((UnitOfWork)_recipeRepository);
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
        private void AddRecipe(string nameRecipe,string category, string subcategories, string description, List<string>ingredients, List<string> countIngred, List<string>recipes)
        {
            var Recipes = _recipeRepository.RecipesRepository.Get();
            foreach (var recip in Recipes)
            {
                if(recip.Name==nameRecipe)
                {
                    Console.WriteLine("Такой рецепт уже существует.");
                }
            }
            
            Recipe r = new Recipe(nameRecipe,category ,subcategories, description, ingredients, countIngred, recipes);
            _recipeRepository.RecipesRepository.Insert(r ?? throw new ArgumentNullException("Нельзя добавить пустой рецепт.",nameof(recipes)));
            CurrentRecipes = r;
        }
        /// <summary>
        /// Поиск рецепта.
        /// </summary>
        /// <param name="nameRecipes">Название рецепта.</param>
        /// <return>Истина, есть ли такой рецепт.</return>
        private bool FindRecipe(string nameRecipes)
        {
            for(int recipe=0; recipe< _recipeRepository.RecipesRepository.Get().Count;recipe++)
            {
                if(_recipeRepository.RecipesRepository.Get()[recipe].Name.ToLower() ==nameRecipes.ToLower())
                {
                    CurrentRecipes = _recipeRepository.RecipesRepository.Get()[recipe];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Открытие рецепта.
        /// </summary>
        /// <param name="categoryControler">Контролер категориями.</param>
        /// <param name="ingradientControler">Контролер ингредиентами.</param>
        public void OpenRecipe(ref CategoryControler categoryControler, ref IngradientControler ingradientControler)
        {
            string str;
            do
            {
                Console.Clear();
                Console.Write("Введите название рецeпта : ");
                str = Console.ReadLine();
                if (str.ToLower() == "bye" || str.ToLower() == "back") return;
            } while (!FindRecipe(str));

            foreach (var category in categoryControler.GetCategories())
            {
                if (category.Name == CurrentRecipes.Category)
                    categoryControler.CurrentCategories = category;        // Устанавливаем активную категорию
            }

            if (!string.IsNullOrWhiteSpace(CurrentRecipes.Name))
            {
                foreach (var subcategory in categoryControler.CurrentCategories.Subcategories)
                {
                    if (subcategory == CurrentRecipes.Subcategory)
                        categoryControler.CurrentCategories.CurrentSubcategories = subcategory;        // Устанавливаем активную подкатегорию
                }
                DisplayCurrentRicepe();         // Показывает рецепт
            }
            else
            {
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Такого рецепта нет, создать ли ?\n" +
                    "1. да\n" +
                    "2. нет\n" +
                    "(number): ");
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                AddRecipe(ref categoryControler,ref ingradientControler);
                                break;
                            case 2:
                                return;
                        }
                    }
                }
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
            for (int ingradient = 0; ingradient < CurrentRecipes.Ingredients.Count; ingradient++)
            {
                Console.WriteLine((ingradient + 1).ToString() + $". {CurrentRecipes.Ingredients[ingradient]} - {CurrentRecipes.CountIngredients[ingradient]} ");
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
        /// Добавление нового рецепта.
        /// </summary>
        public void AddRecipe(ref CategoryControler categoryControler, ref IngradientControler ingradientControler)
        {
            Console.Clear();

            Console.WriteLine("Введите название рецепта: ");
            var name = Console.ReadLine();

            categoryControler.SetCurrentCategory();

            categoryControler.AddSubcategoris();
            var subcategories = categoryControler.CurrentCategories.CurrentSubcategories;
            Console.Clear();
            Console.WriteLine("Введите описание блюда: ");
            var description = Console.ReadLine();

            string str;
            ingradientControler.AddIngradients(out List<string> ingradients);
            Console.WriteLine("\t\t*enter*");
            Console.Clear();

            List<string> countIngred = new List<string>();
            for (int i = 0; i < ingradients.Count; i++)
            {
                Console.WriteLine($"Введите колличество для \"{ingradients[i]}\": ");
                countIngred.Add(Console.ReadLine());
            }

            int steps;
            do
            {
                Console.WriteLine();
                Console.Clear();
                Console.Write("Введите колличество шагов приготовления: ");
                str = Console.ReadLine();
                if (int.TryParse(str, out steps))
                {
                    break;
                }
            } while (true);
            List<string> recipes = new List<string>();

            Console.WriteLine();
            for (int count = 1; count <= steps; count++)
            {
                Console.WriteLine($"Введите описания шага {count} : ");
                str = Console.ReadLine();
                recipes.Add(str);
            }
            AddRecipe(name, categoryControler.CurrentCategories.Name, subcategories, description, ingradients, countIngred, recipes);
            Save();
        }
        /// <summary>
        /// Метод для выбора пользователем конкретного рецепта из списка.
        /// </summary>
        /// <param name="categoryControler">Контролер категориями.</param>
        public bool WalkRecipes(ref CategoryControler categoryControler)
        {
            string str;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                var ListRecipes = new List<Recipe>(); // Список рецептов активной подкатегории
                for (int recipe = 0, count = 1; recipe < GetRecipes().Count; recipe++)
                {
                    if (GetRecipes()[recipe].Subcategory == categoryControler.CurrentCategories.CurrentSubcategories
                       & GetRecipes()[recipe].Category == categoryControler.CurrentCategories.Name)
                    {
                        Console.WriteLine($"{count++}. {GetRecipes()[recipe].Name}");
                        ListRecipes.Add(GetRecipes()[recipe]);
                    }
                }

                Console.WriteLine("Рецепт (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    CurrentRecipes = ListRecipes[result - 1];
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
