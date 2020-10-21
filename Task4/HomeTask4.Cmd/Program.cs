using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Extensions;
using HomeTask4.SharedKernel.Interfaces;
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
            var unitOfWork = host.Services.GetRequiredService<IUnitOfWork>();
            var categoryController = host.Services.GetRequiredService<CategoryController>();
            var subcategoryController = host.Services.GetRequiredService<SubcategoryController>();
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
                                await BookRecipes(categoryController, subcategoryController, recipeController);
                                break;
                            case 2:
                                await ProgramSettings(categoryController,subcategoryController,ingredientController,recipeController);
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

        public static async Task BookRecipes(CategoryController categoryController, SubcategoryController subcategoryController, RecipeController recipeController, string answer = "", int count = 0, int iterator = 0,bool back = false)
        {
            while (true)
            {
                if (back) break;
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                switch (iterator)
                {
                    case 0:
                        #region Walk into categories
                        foreach (var category in await categoryController.GetCategoriesAsync())
                        {
                            Console.WriteLine(++count + ". " + category.Name);
                        }
                        count = 0;
                        Console.WriteLine("Категория (id):");
                        answer = Console.ReadLine();
                        if (IsExit(answer))
                        {
                            answer = "";
                            back = true;
                        }
                        if (await categoryController.WalkCategoriesAsync(answer))
                        {
                            iterator++;
                        }

                        #endregion
                        break;
                    case 1:
                        #region Walk into subcategory

                        var subcategories = await subcategoryController.GetSubcategoriesAsync();
                        foreach (var subcategory in subcategories
                            .Where(c => c.ParentId == categoryController.CurrentCategory.Id).ToList())
                        {
                            Console.WriteLine($"{++count}." + $" {subcategory.Name}");
                            subcategoryController.AddCurrentSubcategoriesInCategory(subcategory.Id);
                        }
                        count = 0;
                        Console.WriteLine("Подкатегория (id):");
                        answer = Console.ReadLine();
                        IsExit(answer);
                        if (await subcategoryController.WalkSubcategoriesAsync(answer))
                        {
                            subcategoryController.ClearCurrentSubcategoriesInCategory();
                            iterator++;
                        }
                        else
                        {

                            iterator--;
                        }
                        break;
                    #endregion
                    case 2:
                        #region Walk into Recipes
                        var getRecipes = await recipeController.GetRecipesAsync();
                        var listRecipes = new List<Recipe>(); // Список рецептов активной подкатегории

                        for (int recipe = 0; recipe < getRecipes.Count; recipe++)
                        {
                            if (getRecipes[recipe].Category.Id == subcategoryController.CurrentSubcategory.Id)
                            {
                                Console.WriteLine($"{++count}. {getRecipes[recipe].Name}");
                                listRecipes.Add(getRecipes[recipe]);
                            }
                        }
                        count = 0;
                        Console.WriteLine("Рецепт (id):");
                        answer = Console.ReadLine();
                        IsExit(answer);
                        if (recipeController.WalkRecipes(listRecipes, answer))
                        {
                            iterator++;
                        }
                        else
                        {
                            iterator--;
                        }
                        break;
                    #endregion
                    case 3:
                        #region Open current recipe
                        Console.Clear();

                        Console.WriteLine("Название : " + recipeController.CurrentRecipe.Name);
                        Console.WriteLine("Описание :" + recipeController.CurrentRecipe.Description);

                        Console.WriteLine("Ингридиенты : ");

                        var ingredients = recipeController.CurrentRecipe.Ingredients;
                        foreach (var ingredient in ingredients)
                        {
                            Console.WriteLine(++count + ". " + ingredient.Ingredient.Name + " - " + ingredient.CountIngredient);
                        }
                        count = 0;
                        Console.WriteLine("Шаги приготовления : ");

                        var steps = recipeController.CurrentRecipe.StepsHowCooking;

                        foreach (var step in steps)
                        {
                            Console.WriteLine(++count + ". " + step.Description);
                        }
                        count = 0;
                        Console.WriteLine("\n\t\t*enter*");
                        Console.ReadLine();
                        iterator--;
                        #endregion
                        break;
                }
            }
        }
        /// <summary>
        /// Метод для отбражения настроек книги рецептов и поиска элементов книги.
        /// </summary>
        public static async Task ProgramSettings(CategoryController categoryController,SubcategoryController subcategoryController,IngredientController ingredientController ,RecipeController recipeController ,string answer = "", int count = 0)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    "1. Список ингредиентов.\n" +
                    "2. Добавить подкатегорию.\n" +
                    "3. Добавить рецепт.\n" +
                    "4. Добавить ингредиент.\n" +
                    "5. Поиск\n" +
                    "6. Вернуться.\n" +
                    "7. Выйти.");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case 1:
                            foreach (var ingredient in await ingredientController.GetIngredientsAsync())
                            {
                                Console.WriteLine($"{++count}. {ingredient.Name}.");
                            }
                            count = 0;
                            Console.WriteLine("\t\t*enter*");
                            Console.ReadLine();
                            break;
                        case 2: //Добавление подкатегории
                            #region Add subcategory
                            Console.Clear();

                            foreach (var category in await categoryController.GetCategoriesAsync())
                            {
                                Console.WriteLine(++count + ". " + category.Name);
                            }
                            count = 0;

                            Console.Write("Ввидите категорию блюда (id) : ");
                            await categoryController.SetCurrentCategoryAsync(Console.ReadLine()); //Выбираем категорию, в которую хотим добавить подкатегорию     

                            Console.Clear();

                            var subcategories = await subcategoryController.GetSubcategoriesAsync();
                            foreach (var subcategory in subcategories
                                .Where(c => c.ParentId == categoryController.CurrentCategory.Id))
                            {
                                Console.WriteLine($"{++count}." + $" {subcategory.Name}");
                            }
                            count = 0;

                            Console.WriteLine("Ввидите название подкатегории блюда (Украинская кухня): ");
                            var newSubcategory = await subcategoryController.AddSubcategoryAsync(categoryController.CurrentCategory.Id, Console.ReadLine());//Создаем или добавляем подкатегорию
                            if (newSubcategory == null) Console.WriteLine("Такая подкатегория уже есть.");
                            #endregion
                            break;
                        case 3: //Добавления рецепта
                            #region add recipe
                            Console.Clear();

                            Console.WriteLine("Введите название рецепта: ");
                            var name = Console.ReadLine();

                            foreach (var category in await categoryController.GetCategoriesAsync())
                            {
                                Console.WriteLine(++count + ". " + category.Name);
                            }
                            count = 0;
                            Console.Write("Ввидите категорию блюда (id) : ");
                            await categoryController.SetCurrentCategoryAsync(Console.ReadLine());

                            Console.Clear();

                            var tempSubcategories = await subcategoryController.GetSubcategoriesAsync();
                            foreach (var subcategory in tempSubcategories
                                .Where(c => c.ParentId == categoryController.CurrentCategory.Id))
                            {
                                Console.WriteLine($"{++count}." + $" {subcategory.Name}");
                            }
                            count = 0;
                            Console.WriteLine("Ввидите название подкатегории блюда (Украинская кухня): ");
                            var subcategoryNew = await subcategoryController.AddSubcategoryAsync(categoryController.CurrentCategory.Id, Console.ReadLine());
                            if (subcategoryNew == null)
                            {
                                throw new ArgumentException(nameof(subcategoryNew), "Null subcategoryNew in new Recipe");
                            }

                            var currentSubcategory = subcategoryController.CurrentSubcategory;
                            Console.Clear();

                            Console.WriteLine("Введите описание блюда: ");
                            var description = Console.ReadLine();
                            Console.Clear();

                            do
                            {
                                Console.WriteLine("Введите колличество ингредиентов: ");
                                if (int.TryParse(Console.ReadLine(), out count))
                                {
                                    break;
                                }
                            } while (true);

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
                            for (int id = 0; id < ingredientsId.Count; id++)
                            {
                                Console.WriteLine($"Введите колличество для " +
                                    $"\"{ingredientController.GetIngredientsAsync().GetAwaiter().GetResult().First(i => i.Id == ingredientsId[id]).Name}\": ");
                                countIngred.Add(Console.ReadLine());
                            }

                            int countSteps = 0;

                            do
                            {
                                Console.WriteLine();
                                Console.Clear();
                                Console.Write("Введите колличество шагов приготовления: ");
                                if (int.TryParse(Console.ReadLine(), out countSteps))
                                {
                                    break;
                                }
                            } while (true);
                            List<string> stepsHowCooking = new List<string>();

                            Console.WriteLine();
                            for (int i = 1; i <= countSteps; i++)
                            {
                                Console.WriteLine($"Введите описания шага {i} : ");
                                answer = Console.ReadLine();
                                stepsHowCooking.Add(answer);
                            }
                            await recipeController.CreateRecipeAsync(name, currentSubcategory.Id, description);
                            await recipeController.AddedIngredientsInRecipeAsync(ingredientsId, countIngred);
                            await recipeController.AddedStepsInRecipeAsync(stepsHowCooking);
                            #endregion
                            break;
                        case 4:
                            #region Add ingredients
                            do
                            {
                                Console.WriteLine("Введите колличество ингредиентов: ");
                                if (int.TryParse(Console.ReadLine(), out count))
                                {
                                    break;
                                }
                            } while (true);

                            ingredientsId = new List<int>();
                            for (int i = 1; i <= count; i++)
                            {
                                Console.WriteLine("Введите ингредиент:");
                                Console.Write($"{i}. ");
                                ingredientsId.Add(await ingredientController.AddedIfNewAsync(Console.ReadLine()));
                            }
                            #endregion
                            break;
                        case 5:
                            #region find
                            Console.Clear();
                            Console.Write(
                                "1. Поиск рецепта.\n" +
                                "2. Поиск ингредиента.\n" +
                                "3. Вернуться.\n" +
                                "4. Выйти.\n" +
                                "(number):");
                            if (int.TryParse(Console.ReadLine(), out result))
                            {
                                switch (result)
                                {
                                    case 1:
                                        #region Find Recipe
                                        Console.Clear();

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

                                            Console.WriteLine("Название : " + recipeController.CurrentRecipe.Name);
                                            Console.WriteLine("Описание :" + recipeController.CurrentRecipe.Description);

                                            Console.WriteLine("Ингридиенты : ");

                                            var ingredients = recipeController.CurrentRecipe.Ingredients;
                                            foreach (var ingredient in ingredients)
                                            {
                                                Console.WriteLine(++count + ". " + ingredient.Ingredient.Name + " - " + ingredient.CountIngredient);
                                            }
                                            count = 0;
                                            Console.WriteLine("Шаги приготовления : ");

                                            var steps = recipeController.CurrentRecipe.StepsHowCooking;

                                            foreach (var step in steps)
                                            {
                                                Console.WriteLine(++count + ". " + step.Description);
                                            }
                                            count = 0;
                                            Console.WriteLine("\n\t\t*enter*");
                                            Console.ReadLine();
                                        }
                                        #endregion
                                        break;
                                    case 2:
                                        #region Find Ingredient
                                        Console.Clear();
                                        Console.Write("Введите название ингредиента : ");
                                        var ingr =  await ingredientController.FindAndGetIngredientAsync(Console.ReadLine().ToLower());
                                        if (ingr != null)
                                        {
                                            Console.WriteLine(ingr.Name + " есть в списке.");
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
                                                                if (int.TryParse(Console.ReadLine(), out countIngredients))
                                                                {
                                                                    break;
                                                                }
                                                            } while (true);

                                                            ingredientsId = new List<int>();
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
                            #endregion
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
