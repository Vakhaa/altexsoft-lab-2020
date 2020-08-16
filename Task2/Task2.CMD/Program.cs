using System;
using System.Collections.Generic;
using Task2.BL.Controler;
using Task2.BL.Model;

namespace Task2.CMD
{
    class Program
    {
        //TODO1 Добавить экземпляры RecipesControler и IngradientController, что б создавть ингредиенты
        //TODO2 Сделать модель категории, что будет содержать в себе подкатегории, а дальше связать подкатегорию с внешним ключом рецепта
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            /*     LoadRecipes();
                 Console.ReadLine();
                 Console.Clear();
                 SaveRecipes();
                 LoadRecipes();
            */
            IngradientControler ic = new IngradientControler();
            ic.ADDIngradients("czlenix");
            ic.Save();
            Console.WriteLine(ic.FindIngrandients("czlenix").Name);
            Console.ReadLine();
        }
        public static void LoadRecipes()
        {
            #region Загрузка с файла
            RecipesControler rc1 = new RecipesControler();
            if (rc1.Recipes.Count != 0)
            {
                Recipe r1 = rc1.Recipes[2];
                Console.WriteLine("Название " + r1.Name);
                Console.WriteLine(r1.Ingredients[0]);
                Console.WriteLine(r1.Ingredients[1]);
                Console.WriteLine("Шаг первый " + r1.StepsHowCooking[0]);
                Console.WriteLine("Шаг второй " + r1.StepsHowCooking[1]);
            }
            #endregion
        }
        public static void SaveRecipes() //tut todo1
        {
            #region Создание рецпета
            Console.WriteLine("Ввидите название рецепта: ");
            var name = Console.ReadLine();

            Console.WriteLine("Ввидите категорию блюда: ");
            var categories = Console.ReadLine();

            Console.WriteLine("Ввидите описание блюда: ");
            var description = Console.ReadLine();

            string str;
            int countIngr;
            do
            {
                Console.WriteLine("Ввидите колличество ингредиентов: ");
                str = Console.ReadLine();
            } while (int.TryParse(str, out countIngr));

            List<string> Ingradients = new List<string>();
            for (int count = 1; count <= countIngr; count++)
            {
                Console.WriteLine("Ввидите ингредиент:");
                Console.Write($"{count}. ");
                Ingradients.Add(Console.ReadLine());
            }

            int steps;
            do
            {
                Console.WriteLine("Ввидете колличество шагов приготовления: ");
                str = Console.ReadLine();
            } while (int.TryParse(str, out steps));
            List<string> recipes = new List<string>();

            for (int count = 1; count <= steps; count++)
            {
                Console.WriteLine($"Ввидете описания шага {count} : ");
                recipes.Add(Console.ReadLine());
            }

            RecipesControler rc = new RecipesControler();
            rc.AddRecipes(name, categories, description, Ingradients, recipes);
            rc.Save();
            #endregion
        }
    }
}
