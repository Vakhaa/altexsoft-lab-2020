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
        private IngradientControler _ingradientControler;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public ConsoleManager(CategoryControler categoryControler, RecipesControler recipesControler, IngradientControler ingradientControler)
        {
            _categoryControler = categoryControler;
            _recipesControler = recipesControler;
            _ingradientControler = ingradientControler;
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
                    while(!_recipesControler.WalkRecipes(ref _categoryControler))
                    {
                        _recipesControler.DisplayCurrentRicepe();
                    }
                }
            }
        }
        /// <summary>
        /// Метод для отбражения настроек книги рецептов и поиска элементов книги.
        /// </summary>
        public void Settings()
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
                            _ingradientControler.DisplayIngradients();
                            break;
                        case 2:
                            _categoryControler.AddSubcategoris();
                            break;
                        case 3:
                            _recipesControler.AddRecipe(ref _categoryControler, ref _ingradientControler);
                            break;
                        case 4:
                            _ingradientControler.AddIngradients(out List<string> ingradients);
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

                        _recipesControler.OpenRecipe(ref _categoryControler,ref _ingradientControler);
                        break;
                    case 2:
                        _ingradientControler.FindIngradient();
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