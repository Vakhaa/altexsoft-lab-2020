using System;
using System.Collections.Generic;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Главный контроллер, для управления элементами логики.
    /// </summary>
    public class ConsoleManager
    {
        /// <summary>
        /// Указатель на контроллер категорий.
        /// </summary>
        private CategoryControler _categoryControler;
        /// <summary>
        /// Указать на контроллер рецептов.
        /// </summary>
        private RecipesControler _recipesControler;
        /// <summary>
        /// Указатель на кнотроллер ингредиентов.
        /// </summary>
        private IngredientControler _ingredientControler;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public ConsoleManager(CategoryControler categoryControler, RecipesControler recipesControler, IngredientControler ingredientControler)
        {
            _categoryControler = categoryControler;
            _recipesControler = recipesControler;
            _ingredientControler = ingredientControler;
        }
        /// <summary>
        /// Метод для отображения логики: категории->подкатегории->рецепт.
        /// </summary>
        public void WalkBook()
        {
            while(!_categoryControler.WalkCategories())
            {
                while(!_categoryControler.WalkSubcategories())
                {
                    while(!_recipesControler.WalkRecipes( _categoryControler.CurrentCategories))
                    {
                        _recipesControler.DisplayCurrentRicepe();
                    }
                }
            }
        }
        /// <summary>
        /// Метод для отбражения настроек книги рецептов и поиска элементов книги.
        /// </summary>
        public void Settings(string str = "")
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("1. Список ингредиентов.\n" +
                    "2. Добавить подкатегорию.\n" +
                    "3. Добавить рецепт.\n" +
                    "4. Добавить ингредиент.\n" +
                    "5. Поиск\n" +
                    "6. Вернуться.\n" +
                    "7. Выйти.");
                if(int.TryParse(Console.ReadLine(), out int result))
                {
                    switch(result)
                    {
                        case 1:
                            _ingredientControler.DisplayIngredients();
                            break;
                        case 2:
                            _categoryControler.AddSubcategoris();
                            break;
                        case 3:
                            Console.Clear();

                            Console.WriteLine("Введите название рецепта: ");
                            var name = Console.ReadLine();

                            _categoryControler.AddSubcategoris();

                            var subcategories = _categoryControler.CurrentCategories.CurrentSubcategories;
                            Console.Clear();
                            
                            Console.WriteLine("Введите описание блюда: ");
                            var description = Console.ReadLine();
                            Console.Clear();

                            var ingredients = _ingredientControler.AddIngredients();
                            Console.WriteLine("\t\t*enter*");
                            Console.Clear();
                            
                            List<string> countIngred = new List<string>();
                            for (int i = 0; i < ingredients.Count; i++)
                            {
                                Console.WriteLine($"Введите колличество для \"{ingredients[i]}\": ");
                                countIngred.Add(Console.ReadLine());
                            }

                            int steps = 0;

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
                            _recipesControler.AddRecipe(name, _categoryControler.CurrentCategories.Name, subcategories, description, ingredients, countIngred, recipes);
                            _recipesControler.Save();
                            break;
                        case 4:
                            _ingredientControler.AddIngredients();
                            break;
                        case 5:
                            Find();
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
        /// <param name="str">Строка для обработки ответа пользователя.</param>
        /// <returns>Истина, хочет ли выйти пользователь.</returns>
        public static bool IsExit(string str)
        {
            switch (str.ToLower())
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
        /// Метод для отображение методов поиска элементов.
        /// </summary>
        private void Find()
        {
            Console.Clear();
            Console.Write("1. Поиск рецепта.\n" +
                "2. Поиск ингредиента.\n" +
                "3. Вернуться.\n" +
                "4. Выйти.\n" +
                "(number):");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                switch (result)
                {
                    case 1:
                        var currentCategory = _categoryControler.CurrentCategories;
                        _recipesControler.OpenRecipe(_categoryControler.GetCategories(), ref currentCategory); // по ссылке, что бы установить активную категорию
                        break;
                    case 2:
                        _ingredientControler.FindIngredient();
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
    }
}