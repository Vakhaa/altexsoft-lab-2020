using System;
using System.Collections.Generic;
using Task2.BL.Model;

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
        public ConsoleManager()
        {
            _categoryControler = new CategoryControler();
            _recipesControler = new RecipesControler();
            _ingradientControler = new IngradientControler();
        }
        /// <summary>
        /// Метод для отображения логики: категории->подкатегории->рецепт.
        /// </summary>
        public void WalkBook()
        {
            while(!WalkCategories())
            {
                while(!WalkSubcategories())
                {
                    while(!WalkRecipes())
                    {
                        DisplayCurrentRicepe();
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
                            DisplayIngradients();
                            break;
                        case 2:
                            AddSubcategoris();
                            break;
                        case 3:
                            AddRecipe();
                            break;
                        case 4:
                            AddIngradient(out List<string> ingradients);
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
        public bool isExit(string str)
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

        #region Private method

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
                        FindRecipes();
                        break;
                    case 2:
                        FindIngradient();
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
        /// <summary>
        /// Поиск рецептов.
        /// </summary>
        private void FindRecipes()
        {
            string str;
            do
            {
                Console.Clear();
                Console.Write("Ввидите название рецeпта : ");
                str =Console.ReadLine();
                if (str.ToLower() == "bye" || str.ToLower() == "back") return; 
            } while (!_recipesControler.FindRecipes(str));

            foreach (var category in _categoryControler.Categories)         
            {
                if (category.Name == _recipesControler.CurrentRecipes.Category)
                    _categoryControler.CurrentCategoties = category;        // Устанавливаем активную категорию
            }

            if(!string.IsNullOrWhiteSpace(_recipesControler.CurrentRecipes.Name))
            {
                foreach(var subcategory in _categoryControler.CurrentCategoties.Subcategories)
                {
                    if (subcategory == _recipesControler.CurrentRecipes.Subcategory) 
                        _categoryControler.CurrentCategoties.CurrentSubcategories = subcategory;        // Устанавливаем активную подкатегорию
                }
                DisplayCurrentRicepe();         // Показывает рецепт
            }
            else
            {
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Такого рецепта нет, создать ли ?\n" +
                    "1. yes\n" +
                    "2. no\n" +
                    "(number): ");
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                AddRecipe();
                                break;
                            case 2:
                                return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Поиск ингредиентов.
        /// </summary>
        private void FindIngradient()
        {
            Console.Clear();
            Console.Write("Ввидите название ингредиента : ");
            var ingr = _ingradientControler.FindIngrandients(Console.ReadLine());
            if(ingr!=null)
            {
                Console.WriteLine(ingr.Name+" есть в списке.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Такого ингредиента нет, создать ли ?\n" +
                    "1. yes\n" +
                    "2. no\n" +
                    "(number): ");
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                AddIngradient(out List<string> ingradients);
                                return;
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
        private void DisplayCurrentRicepe()
        {
            Console.Clear();
            Console.WriteLine("Название : " + _recipesControler.CurrentRecipes.Name);
            Console.WriteLine(_recipesControler.CurrentRecipes.Description);

            Console.WriteLine("Ингридиенты : ");
            for (int ingradient = 0; ingradient < _recipesControler.CurrentRecipes.Ingredients.Count; ingradient++)
            {
                Console.WriteLine((ingradient + 1).ToString() + $". {_recipesControler.CurrentRecipes.Ingredients[ingradient]} - {_recipesControler.CurrentRecipes.CountIngredients[ingradient]} ");
            }

            Console.WriteLine("Шаги приготовления : ");
            for (int step = 0; step < _recipesControler.CurrentRecipes.StepsHowCooking.Count; step++)
            {
                Console.WriteLine((step + 1).ToString() + $". {_recipesControler.CurrentRecipes.StepsHowCooking[step]}");
            }
            Console.WriteLine("\n\t\t*enter*");
            Console.ReadLine();
            //return WalkRecipes();
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной категории из списка.
        /// </summary>
        /// <returns>Истина, если пользователь выходит, то он выходит в главное меню.</returns>
        private bool WalkCategories()
        {
            string str;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplayCategory();
                Console.WriteLine("Категория (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    _categoryControler.CurrentCategoties = _categoryControler.Categories[result - 1];
                    return false;
                }
                else
                {
                    if(isExit(str))
                    {
                        return true;
                    }
                }
                Console.WriteLine("\tSome mistake, try again...");
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        private bool WalkSubcategories()
        {
            string str;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplaySubcategory();
                Console.WriteLine("Подкатегория (id):");
                    str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    _categoryControler.CurrentCategoties.CurrentSubcategories = _categoryControler.CurrentCategoties.Subcategories[result - 1];
                    return false;
                }
                else
                {
                    if(isExit(str))
                    {
                        return true;
                    }
                }
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретного рецепта из списка.
        /// </summary>
        private bool WalkRecipes()
        {
            string str;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                var ListRecipes = new List<Recipe>(); // Список рецептов активной подкатегории
                for (int recipe = 0,count=1; recipe < _recipesControler.Recipes.Count; recipe++)
                {
                    if(_recipesControler.Recipes[recipe].Subcategory == _categoryControler.CurrentCategoties.CurrentSubcategories
                       & _recipesControler.Recipes[recipe].Category==_categoryControler.CurrentCategoties.Name)
                    {
                        Console.WriteLine($"{count++}. {_recipesControler.Recipes[recipe].Name}");
                        ListRecipes.Add(_recipesControler.Recipes[recipe]);
                    }
                }

                Console.WriteLine("Рецепт (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    _recipesControler.CurrentRecipes = ListRecipes[result - 1];
                    return false;
                }
                else
                {
                    if(isExit(str))
                    {
                        return true;
                    }
                }
                Console.WriteLine("\tSome mistake, try again...");
            }
        }
        /// <summary>
        /// Добавление нового рецепта.
        /// </summary>
        private void AddRecipe()
        {
            Console.Clear();

            Console.WriteLine("Ввидите название рецепта: ");
            var name = Console.ReadLine();

            SetCurrentCategory();

            AddSubcategoris();
            var subcategories = _categoryControler.CurrentCategoties.CurrentSubcategories;
            Console.Clear();
            Console.WriteLine("Ввидите описание блюда: ");
            var description = Console.ReadLine();

            string str;
            AddIngradient(out List<string> ingradients);
            Console.WriteLine("\t\t*enter*");
            Console.Clear();

            List<string> countIngred = new List<string>();
            for(int i=0; i<ingradients.Count;i++)
            {
                Console.WriteLine($"Ввидите колличество для \"{ingradients[i]}\": ");
                countIngred.Add(Console.ReadLine());
            }

            int steps;
            do
            {
                Console.WriteLine();
                Console.Clear();
                Console.Write("Ввидете колличество шагов приготовления: ");
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
                Console.WriteLine($"Ввидете описания шага {count} : ");
                str=Console.ReadLine();
                recipes.Add(str);
            }
            _recipesControler.AddRecipes(name, _categoryControler.CurrentCategoties.Name, subcategories, description, ingradients, countIngred, recipes);
            _recipesControler.Save();
        }
        /// <summary>
        /// Установка конкретной категории
        /// </summary>
        private void SetCurrentCategory()
        {
            bool _isExist = false;
            int categories;
            while (true)
            {
                Console.Clear();
                DisplayCategory();
                Console.Write("Ввидите категорию блюда (id) : ");
                if(int.TryParse(Console.ReadLine(), out categories))
                foreach (var category in _categoryControler.Categories)
                {
                    if (category.Name == _categoryControler.Categories[categories-1].Name)
                    {
                        _categoryControler.CurrentCategoties = category;
                        _isExist = true;
                    }
                }
                if (_isExist)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Добавления новой подкатегории
        /// </summary>
        /// <param name="subcategories"></param>
        private void AddSubcategoris()
        {
            Console.Clear();
            SetCurrentCategory();
            Console.Clear();
            DisplaySubcategory();
            Console.WriteLine("Ввидите подкатегорию блюда (названия): ");
            var str = Console.ReadLine();
            if (!_categoryControler.CurrentCategoties.FindSubcategory(str))
            {
                _categoryControler.CurrentCategoties.AddSubcategories(str);
                _categoryControler.Save();
            }
            else
            {
                Console.WriteLine("Такая подкатегория уже есть.");
            }
        }
        /// <summary>
        /// Добавление ингредиента
        /// </summary>
        /// <param name="ingradients">Список новых ингредиентов</param>
        private void AddIngradient(out List<string> ingradients) 
        {
            string str;
            int result;
            do
            {
                Console.WriteLine("Ввидите колличество ингредиентов: ");
                str = Console.ReadLine();
                if (int.TryParse(str, out result))
                {
                    break;
                }
            } while (true);

            ingradients = new List<string>();
            for (int count = 1; count <= result; count++)
            {
                Console.WriteLine("Ввидите ингредиент:");
                Console.Write($"{count}. ");
                str = Console.ReadLine();
                ingradients.Add(str);
                _ingradientControler.ADDIngradients(str);
                _ingradientControler.Save();
            }
            Console.ReadLine();
        }
        /// <summary>
        /// Метод для отображения ингредиентов
        /// </summary>
        private void DisplayIngradients()
        {
            foreach (var ingradient in _ingradientControler.Ingradients)
            {
                Console.WriteLine(ingradient.Name);
            }
            Console.WriteLine("\t\t*enter*");
            Console.ReadLine();
        }
        /// <summary>
        /// Метод для отображения категорий
        /// </summary>
        private void DisplayCategory()
        {
            int count = 1;
            foreach (var category in _categoryControler.Categories)
            {
                Console.WriteLine(count.ToString()+". "+category.Name);
                count++;
            }
        }
        /// <summary>
        /// Метод для отображения подкатегорий
        /// </summary>
        private void DisplaySubcategory()
        {
            for (int subcategory = 0; subcategory < _categoryControler.CurrentCategoties.Subcategories.Count; subcategory++)
            {
                Console.WriteLine($"{subcategory + 1}. {_categoryControler.CurrentCategoties.Subcategories[subcategory]}");
            }
        }
        #endregion
    }
}