using System;
using System.Collections.Generic;
using System.Threading;
using Task2.BL.Controler;
using Task2.BL.Model;

namespace Task2.CMD
{
    class Program
    {
        //TODO Сделать модель категории, что будет содержать в себе подкатегории, а дальше связать подкатегорию с внешним ключом рецепта
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LoadRecipes();
            Console.ReadLine();
            Console.Clear();
            SaveRecipes();
            LoadRecipes();
            Console.ReadLine();
        }
        public static void LoadRecipes()
        {
            #region Загрузка с файла
            RecipesControler rc1 = new RecipesControler();
            if (rc1.Recipes.Count != 0)
            {
                Recipes r1 = rc1.Recipes[2];
                Console.WriteLine("Название " + r1.Name);
                Console.WriteLine(r1.Ingredients[0]);
                Console.WriteLine(r1.Ingredients[1]);
                Console.WriteLine("Шаг первый " + r1.StepsHowCooking[0]);
                Console.WriteLine("Шаг второй " + r1.StepsHowCooking[1]);
            }
            #endregion
        }
        public static void SaveRecipes()
        {
            #region Создание рецпета
            Console.WriteLine("Ввидите название рецепта: ");
            var name = Console.ReadLine();

            Console.WriteLine("Ввидите категорию блюда: "); //enum
            var categories = Console.ReadLine();

            Console.WriteLine("Ввидите описание блюда: ");
            var description = Console.ReadLine();

            Console.WriteLine("Ввидите колличество ингредиентов: ");
            var countIngr = int.Parse(Console.ReadLine()); //proverka
            List<string> Ingradients = new List<string>();

            for (int count = 1; count <= countIngr; count++)
            {
                Console.WriteLine("Ввидите ингредиент:");
                Console.Write($"{count}. ");
                Ingradients.Add(Console.ReadLine());
            }

            Console.WriteLine("Ввидете колличество шагов приготовления: ");
            var steps = int.Parse(Console.ReadLine()); //proverka
            List<string> recipes = new List<string>();

            for (int count = 1; count <= steps; count++)
            {
                Console.WriteLine($"Ввидете описания шага {count} : ");
                recipes.Add(Console.ReadLine());
            }
            Recipes r = new Recipes(name, categories, description, Ingradients, recipes);
            RecipesControler rc = new RecipesControler();
            rc.AddRecipes(ref r);
            rc.Save();
            #endregion
        }
    }
}
