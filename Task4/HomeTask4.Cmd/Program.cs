﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeTask4.Cmd
{
    public sealed class Program
    {

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            var categoryController = host.Services.GetRequiredService<CategoryController>();
            var ingredientController = host.Services.GetRequiredService<IngredientController>();
            var recipeController = host.Services.GetRequiredService<RecipeController>();

            while (true) //главное меню программы
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine(
                    "1. Книга рецептов.\n" +
                    "2. Настройка книги.\n" +
                    "3. Выйти.");
                    if (int.TryParse(Console.ReadLine(), out int result)) //обработка ответа
                    {
                        switch (result)
                        {
                            case 1:
                                await BookRecipes(categoryController, recipeController);
                                break;
                            case 2:
                                await ProgramSettings(categoryController,ingredientController,recipeController);
                                break;
                            case 3:
                                logger.LogInformation("Have a nice day! =)");
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Ошибка в вводе данных.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка в вводе данных.");
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    logger.LogError(e.Message);
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        public static async Task BookRecipes(CategoryController categoryController, RecipeController recipeController)
        {
            while(true)
            {
                await WalkIntoBookRecipesAsync(categoryController, recipeController);
                if (categoryController.LevelImmersion == -1)
                {
                   categoryController.LevelImmersion = 0;
                   break;
                }
            }
        }
        public static async Task WalkIntoBookRecipesAsync(CategoryController categoryController, RecipeController recipeController, string answer="")
        {
            await DisplayComponents(categoryController,recipeController);
            answer = Console.ReadLine().ToLower();
            if (IsExit(answer))
            {
                categoryController.LevelImmersion--;
                if(categoryController.CurrentCategory!=null)
                categoryController.CurrentCategory = categoryController.CurrentCategory.Parent;
                return;
            }
            if(!answer.Contains("open"))
            {
                if (await categoryController.WalkCategoriesAsync(answer))
                {
                    if (categoryController.CurrentCategory.Children != null)
                    {
                        categoryController.LevelImmersion++;
                        await WalkIntoBookRecipesAsync(categoryController, recipeController);
                    }
                }
            }
            else
            {
                if (int.TryParse(Regex.Match(answer, ".[1-9].*").ToString(), out int recipeId))
                {
                    recipeController.CurrentRecipe = await recipeController.FindRecipeAsync(recipeId);
                    if(recipeController.CurrentRecipe.Category.Id == categoryController.CurrentCategory.Id)
                    await OpenCurrentRecipeAsync(recipeController);
                }
                else
                {
                    Console.WriteLine("Некорректно веден формат: \"open x (где x - целое число).\"");
                    Console.WriteLine("\t\t**enter**");
                    Console.ReadLine();
                }
            }
            
        }
        public static async Task DisplayComponents(CategoryController categoryController, RecipeController recipeController, bool checkRecipe = false)
        {
            Console.Clear();
            Console.WriteLine("\t\t*exit: bye, back: back, open recipe: open id*");
            if (categoryController.LevelImmersion == 0)
                foreach (var category in await categoryController.GetCategoriesAsync())
                {
                    Console.WriteLine($"{category.Id}. {category.Name}");
                }
            else
                foreach (var child in await categoryController.GetCurrentChildAsync())
                {
                    Console.WriteLine($"{child.Id}. {child.Name}");
                }

            Console.WriteLine("Категория (id):");

            if (categoryController.LevelImmersion != 0)
                foreach (var recipe in await recipeController.GetRecipesAsync())
                {
                    if (recipe.Category.Id == categoryController.CurrentCategory.Id)
                    {
                        checkRecipe = true;
                        Console.WriteLine($"{recipe.Id}. {recipe.Name}");
                    }
                }
            if (checkRecipe)
                Console.WriteLine("Рецепт (open id):");

        }
        public static async Task OpenCurrentRecipeAsync(RecipeController recipeController, int count = 0)
        {
            Console.Clear();

            Console.WriteLine($"Название : {recipeController.CurrentRecipe.Name}");
            Console.WriteLine($"Описание : {recipeController.CurrentRecipe.Description}");

            Console.WriteLine("Ингридиенты : ");

            var ingredients = recipeController.CurrentRecipe.Ingredients;
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{++count}. {ingredient.Ingredient.Name} - {ingredient.CountIngredient}");
            }
            count = 0;
            Console.WriteLine("Шаги приготовления : ");

            var steps = recipeController.CurrentRecipe.StepsHowCooking;

            foreach (var step in steps)
            {
                Console.WriteLine($"{++count}. {step.Description}");
            }
            Console.WriteLine("\n\t\t*enter*");
            Console.ReadLine();
        }
        /// <summary>
        /// Метод для отбражения настроек книги рецептов и поиска элементов книги.
        /// </summary>
        public static async Task ProgramSettings(CategoryController categoryController, IngredientController ingredientController, RecipeController recipeController, string answer = "", int count = 0)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    "1. Список ингредиентов.\n" +
                    "2. Добавить категорию.\n" +
                    "3. Добавить рецепт.\n" +
                    "4. Добавить ингредиент.\n" +
                    "5. Поиск\n" +
                    "6. Вернуться.\n" +
                    "7. Выйти.");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    Console.Clear();
                    switch (result)
                    {
                        case 1:
                            await ListIngredientsAsync(ingredientController);
                            break;
                        case 2:
                            await AddChildAsync(categoryController);
                            break;
                        case 3:
                            await AddRecipeAsync(categoryController,recipeController,ingredientController);
                            break;
                        case 4:
                            await AddIngredientsAsync(ingredientController);
                            break;
                        case 5:
                            await FindAsync(recipeController,ingredientController);
                            break;
                        case 6:
                            return;
                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Ошибка в вводе данных.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка в вводе данных.");
                }
            }
        }
        public static async Task ListIngredientsAsync(IngredientController ingredientController, int count=0)
        {
            foreach (var ingredient in await ingredientController.GetIngredientsAsync())
            {
                Console.WriteLine($"{++count}. {ingredient.Name}.");
            }
            Console.WriteLine("\t\t*enter*");
            Console.ReadLine();
        }
        public static async Task AddChildAsync(CategoryController categoryController, int count=0)
        {
            foreach (var category in await categoryController.GetCategoriesAsync())
            {
                Console.WriteLine($"{++count}. {category.Name}");
            }
            count = 0;

            Console.Write("Ввидите категорию блюда (id) : ");
            await categoryController.WalkCategoriesAsync(Console.ReadLine()); //Выбираем категорию, в которую хотим добавить подкатегорию     

            Console.Clear();

            var subcategories = categoryController.CurrentCategory.Children;
            foreach (var subcategory in subcategories)
            {
                Console.WriteLine($"{++count}. {subcategory.Name}");
            }
            count = 0;

            Console.WriteLine("Ввидите название подкатегории блюда (Украинская кухня): ");
            var newSubcategory = await categoryController.AddChildAsync(categoryController.CurrentCategory.Id, Console.ReadLine());//Создаем или добавляем подкатегорию
            if (newSubcategory == null) Console.WriteLine("Такая подкатегория уже есть.");
        }
        public static async Task AddRecipeAsync(CategoryController categoryController, RecipeController recipeController, IngredientController ingredientController,string answer="", int count=0)
        {
            Console.WriteLine("Введите название рецепта: ");
            var name = Console.ReadLine();

            DisplayCategoryTree(await categoryController.GetCategoriesAsync());
            Console.Write("Ввидите категорию блюда (id) : ");
            await categoryController.WalkCategoriesAsync(Console.ReadLine());

            Console.Clear();

            var tempChild = categoryController.CurrentCategory.Children;
            foreach (var child in tempChild)
            {
                Console.WriteLine($"{++count}. {child.Name}");
            }
            count = 0;
            Console.WriteLine("Ввидите название подкатегории блюда (Украинская кухня): ");
            var categoryNew = await categoryController.AddChildAsync(categoryController.CurrentCategory.Id, Console.ReadLine());
            if (categoryNew == null)
            {
                throw new ArgumentException(nameof(categoryNew), "Null subcategoryNew in new Recipe");
            }

            Console.Clear();

            Console.WriteLine("Введите описание блюда: ");
            var description = Console.ReadLine();
            Console.Clear();

            do
            {
                Console.WriteLine("Введите колличество ингредиентов: ");
                answer = Console.ReadLine();
            } while (!int.TryParse(answer, out count));

            var ingredientsId = new List<int>();
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine("Введите ингредиент:");
                Console.Write($"{i}. ");
                ingredientsId.Add(await ingredientController.AddedIfNewAsync(Console.ReadLine()));
            }

            Console.WriteLine("\t\t*enter*");
            Console.ReadKey();
            Console.Clear();

            List<string> countIngred = new List<string>();
            var tempIngredients = new List<Ingredient>();
            for (int id = 0; id < ingredientsId.Count; id++)
            {
                tempIngredients = await ingredientController.GetIngredientsAsync();
                Console.WriteLine("Введите колличество для " +
                    $"\"{tempIngredients.First(i => i.Id == ingredientsId[id]).Name}\": ");
                countIngred.Add(Console.ReadLine());
            }

            int countSteps = 0;

            do
            {
                Console.WriteLine();
                Console.Clear();
                Console.Write("Введите колличество шагов приготовления: ");
                answer = Console.ReadLine();
            } while(!int.TryParse(answer, out countSteps));
            List<string> stepsHowCooking = new List<string>();

            Console.WriteLine();
            for (int i = 1; i <= countSteps; i++)
            {
                Console.WriteLine($"Введите описания шага {i} : ");
                answer = Console.ReadLine();
                stepsHowCooking.Add(answer);
            }
            await recipeController.CreateRecipeAsync(name, categoryNew.Id, description);
            await recipeController.AddedIngredientsInRecipeAsync(ingredientsId, countIngred);
            await recipeController.AddedStepsInRecipeAsync(stepsHowCooking);
        }
        public static async Task AddIngredientsAsync(IngredientController ingredientController, string answer="", int count=0)
        {
            do
            {
                Console.WriteLine("Введите колличество ингредиентов: ");
                answer = Console.ReadLine();
            } while (!int.TryParse(answer, out count));

            var ingredientsId = new List<int>();
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine("Введите ингредиент:");
                Console.Write($"{i}. ");
                ingredientsId.Add(await ingredientController.AddedIfNewAsync(Console.ReadLine()));
            }
        }
        public static async Task FindAsync(RecipeController recipeController, IngredientController ingredientController, string answer="", int count=0)
        {
            Console.Write("1. Поиск рецепта.\n" +
                                "2. Поиск ингредиента.\n" +
                                "3. Вернуться.\n" +
                                "4. Выйти.\n" +
                                "(number):");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                Console.Clear();
                switch (result)
                {
                    case 1:
                        #region Find Recipe
                        await FindRecipeAsync(recipeController);
                        #endregion
                        break;
                    case 2:
                        #region Find Ingredient
                        await FindIngredientAsync(ingredientController);
                        #endregion
                        break;
                    case 3:
                        return;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ошибка в вводе данных.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ошибка в вводе данных.");
            }
        }
        public static async Task FindRecipeAsync(RecipeController recipeController,string answer="", int count=0)
        {
            foreach (var recipes in await recipeController.GetRecipesAsync())
            {
                Console.WriteLine($"{recipes.Id}. {recipes.Name}.");
            }

            Console.Write("Введите id рецeпта : ");
            answer = Console.ReadLine();
            if (answer.ToLower() == "bye" || answer.ToLower() == "back") return;
            if (!int.TryParse(answer, out int recipeId)) return;

            recipeController.CurrentRecipe = await recipeController.FindRecipeAsync(recipeId);

            if (!string.IsNullOrWhiteSpace(recipeController.CurrentRecipe.Name))
            {
                Console.Clear();

                Console.WriteLine($"Название : {recipeController.CurrentRecipe.Name}");
                Console.WriteLine($"Описание : {recipeController.CurrentRecipe.Description}");

                Console.WriteLine("Ингридиенты : ");

                var ingredients = recipeController.CurrentRecipe.Ingredients;
                foreach (var ingredient in ingredients)
                {
                    Console.WriteLine($"{++count}. {ingredient.Ingredient.Name} - {ingredient.CountIngredient}");
                }
                count = 0;
                Console.WriteLine("Шаги приготовления : ");

                var steps = recipeController.CurrentRecipe.StepsHowCooking;

                foreach (var step in steps)
                {
                    Console.WriteLine($"{++count}. {step.Description}");
                }
                Console.WriteLine("\n\t\t*enter*");
                Console.ReadLine();
            }
        }
        public static async Task FindIngredientAsync(IngredientController ingredientController, string answer="",int count=0)
        {
            Console.Write("Введите название ингредиента : ");
            var ingr = await ingredientController.FindAndGetIngredientAsync(Console.ReadLine().ToLower());
            if (ingr != null)
            {
                Console.WriteLine($"{ingr.Name} есть в списке.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                while (true)
                {
                    Console.Write(
                    "Такого ингредиента нет, создать ли ?\n" +
                    "1. да\n" +
                    "2. нет\n" +
                    "(number): ");
                    if (int.TryParse(Console.ReadLine(), out int tempResult))
                    {
                        int countIngredients = 0;
                        switch (tempResult)
                        {
                            case 1:
                                do
                                {
                                    Console.WriteLine("Введите колличество ингредиентов: ");
                                    answer = Console.ReadLine();
                                } while (!int.TryParse(answer, out countIngredients));

                                var ingredientsId = new List<int>();
                                for (count = 1; count <= countIngredients; count++)
                                {
                                    Console.WriteLine("Введите ингредиент:");
                                    Console.Write($"{count}. ");
                                    ingredientsId.Add(await ingredientController.AddedIfNewAsync(Console.ReadLine()));
                                }
                                return;
                            case 2:
                                return;
                        }
                    }
                }
            }
        }
        public static void DisplayCategoryTree(List<Category> categories, int LevelLayer=0)
        {
            foreach (var category in categories)
            {
                for(int i=0;i<LevelLayer;i++)
                {
                    Console.Write("\t");
                }
                Console.WriteLine($"{category.Id}. {category.Name}");
                if (category.Children != null)
                    DisplayCategoryTree(category.Children, LevelLayer+1);
            }
        }
        /// <summary>
        /// Метод для обработки выхода с текущей позиций или с программы.
        /// </summary>
        /// <param name="answer">Строка для обработки ответа пользователя.</param>
        /// <returns>Истина, хочет ли выйти пользователь.</returns>
        public static bool IsExit(string answer)
        {
            switch (answer.ToLower())
            {
                case "bye":
                    Environment.Exit(0);
                    break;
                case "back":
                    return true;
            }
            return false;
        }

        /// <summary>
        /// This method should be separate to support EF command-line tools in design time
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns><see cref="IHostBuilder" />hostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureServices((context, services) =>
               {
                   services.AddInfrastructure(context.Configuration.GetConnectionString("Default"));
               })
               .ConfigureLogging(config =>
               {
            config.ClearProviders();
            config.AddConsole();
        });
    }
}
