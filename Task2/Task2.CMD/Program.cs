using System;
using System.Collections.Generic;
using Task2.BL.Controler;
using Task2.BL.Model;

namespace Task2.CMD
{
    class Program
    {
        //TODO Сделать контроллер модели категории,
        //а дальше связать подкатегорию с внешним ключом рецепта, модель категории уже есть
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            IngradientControler ic = new IngradientControler();
            RecipesControler rc = new RecipesControler();
            /*LoadRecipes(rc);
            Console.ReadLine();
            Console.Clear();
            SaveRecipes(ic);
            LoadRecipes(rc);*/
            rc.FindRecipes("Perec wraszyrowanyj");
            Console.WriteLine(rc.CurrentRecipes.Name);
            Console.WriteLine(rc.CurrentRecipes.Description);
            Console.WriteLine(rc.CurrentRecipes.Ingredients[0]);
            Console.WriteLine(rc.CurrentRecipes.Ingredients[1]);

            //    ic.ADDIngradients("czlenix");
            //    ic.Save();
            //    Console.WriteLine(ic.FindIngrandients("czlenix").Name);

            Console.ReadLine();
        }
        public static void LoadRecipes(RecipesControler rc1)
        {
            #region Загрузка с файла
            if (rc1.Recipes.Count != 0)
            {
                Recipe r1 = rc1.Recipes[0];
                Console.WriteLine("Название " + r1.Name);
                Console.WriteLine(r1.Ingredients[0]);
                Console.WriteLine(r1.Ingredients[1]);
                Console.WriteLine("Шаг первый " + r1.StepsHowCooking[0]);
                Console.WriteLine("Шаг второй " + r1.StepsHowCooking[1]);
            }
            #endregion
        }
        public static void SaveRecipes(IngradientControler ic)
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
                if(int.TryParse(str, out countIngr))
                {
                    break;
                }
           } while (true);

            List<string> Ingradients = new List<string>();
            for (int count = 1; count <= countIngr; count++)
            {
                Console.WriteLine("Ввидите ингредиент:");
                Console.Write($"{count}. ");
                str = Console.ReadLine();
                Ingradients.Add(str);
                ic.ADDIngradients(str);
                ic.Save();
          //      Console.WriteLine(ic.FindIngrandients("czlenix").Name);
            }

            int steps;
            do
            {
                Console.WriteLine("Ввидете колличество шагов приготовления: ");
                str = Console.ReadLine();
                if(int.TryParse(str, out steps))
                {
                    break;
                }
            } while (true);
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
