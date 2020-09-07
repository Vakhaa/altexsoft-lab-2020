using System;
using System.Collections.Generic;
using System.Linq;

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
        private CategoryController _categoryControler;
        /// <summary>
        /// Указать на контроллер рецептов.
        /// </summary>
        private RecipesController _recipesControler;
        /// <summary>
        /// Указатель на кнотроллер ингредиентов.
        /// </summary>
        private IngredientController _ingredientControler;

        private SubcategoryController _subcategoryControler;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public ConsoleManager(CategoryController categoryControler, SubcategoryController subcategoryControler ,RecipesController recipesControler, IngredientController ingredientControler)
        {
            _subcategoryControler = subcategoryControler;
            _categoryControler = categoryControler;
            _recipesControler = recipesControler;
            _ingredientControler = ingredientControler;
        }
        /// <summary>
        /// Метод для отображения логики: категории->подкатегории->рецепты->рецепт.
        /// </summary>
        public void WalkBook()
        {
            while(!_categoryControler.WalkCategories())
            {
                while(!_subcategoryControler.WalkSubcategories(_categoryControler.CurrentCategories.SubcategoriesId))
                {
                    while(!_recipesControler.WalkRecipes( _categoryControler.CurrentCategories.Id,_subcategoryControler.CurrentSubcategories.Id))
                    {
                        _recipesControler.DisplayCurrentRicepe(_ingredientControler.GetIngredients());
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
                        case 2: //Добавление подкатегории
                            _categoryControler.SetCurrentCategory(); //Выбираем категорию, в которую хотим добавить подкатегорию     

                            Console.Clear();
                            _subcategoryControler.DisplaySubcategory(_categoryControler.CurrentCategories.SubcategoriesId);
                            var subcategoryNew = _subcategoryControler.AddSubcategory(); //Создаем или добавляем подкатегорию
                            if (subcategoryNew != null)//Если подкатегория пустая, это значит, что в категории уже есть такая подкатегория 
                                _categoryControler.CurrentCategories.SubcategoriesId.Add(subcategoryNew.Id);
                            _categoryControler.Save();

                            break;
                        case 3: //Добавления рецепта
                            Console.Clear();

                            Console.WriteLine("Введите название рецепта: ");
                            var name = Console.ReadLine();

                            _categoryControler.SetCurrentCategory();

                            Console.Clear();
                            _subcategoryControler.DisplaySubcategory(_categoryControler.CurrentCategories.SubcategoriesId);
                            subcategoryNew = _subcategoryControler.AddSubcategory();
                            if (subcategoryNew != null)
                            {
                                _categoryControler.CurrentCategories.SubcategoriesId.Add(subcategoryNew.Id);
                            }
                            else
                            { //Добавляем новоую подкатегорию в категорию, если там ее нет
                                if(!_categoryControler.CurrentCategories.SubcategoriesId.Any(
                                    s=>s == _subcategoryControler.CurrentSubcategories.Id))
                                _categoryControler.CurrentCategories.SubcategoriesId.Add(_subcategoryControler.CurrentSubcategories.Id);
                            }

                            var subcategories = _subcategoryControler.CurrentSubcategories;
                            Console.Clear();
                            
                            Console.WriteLine("Введите описание блюда: ");
                            var description = Console.ReadLine();
                            Console.Clear();

                            
                            var ingredients = _ingredientControler.AddIngredients();
                            
                            Console.WriteLine("\t\t*enter*");
                            Console.Clear();

                            List<string> countIngred = new List<string>();
                            for (int id = 0; id < ingredients.Count; id++)
                            {
                                Console.WriteLine($"Введите колличество для " +
                                    $"\"{_ingredientControler.GetIngredients().First(i => i.Id == ingredients[id]).Name}\": ");
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
                            _recipesControler.AddRecipe(name, _categoryControler.CurrentCategories.Id, subcategories.Id, description, ingredients, countIngred, recipes);
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
                        _recipesControler.OpenRecipe(_ingredientControler.GetIngredients());
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