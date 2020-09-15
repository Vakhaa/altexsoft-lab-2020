using System;
using Task2.BL.Controler;
using Task2.BL.Interfaces;

namespace Task2.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork uow = new UnitOfWork();
            CategoryController cc = new CategoryController(uow);
            SubcategoryController sc = new SubcategoryController(uow);
            cc.DisplayCategory();
            sc.DisplaySubcategory(2);
        }
       /* static void Main(string[] args)
        {
            UnitOfWork uow = new UnitOfWork();

            ConsoleManager cm = new ConsoleManager(
                new CategoryController((ICategoryUnityOfWork)uow), 
                new SubcategoryController(uow),
                new RecipesController((IRecipeUnityOfWork)uow), 
                new IngredientController((IIngredientUnityOfWork)uow));
            
            Console.WriteLine("Hello World!" + "\n\t\t*enter*");
            Console.ReadLine();
            
            while(true) //главное меню программы
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("1. Книга рецептов.\n" +
                    "2. Настройка книги.\n" +
                    "3. Выйти.");
                    if (int.TryParse(Console.ReadLine(), out int result)) //обработка ответа
                    {
                        switch (result)
                        {
                            case 1:
                                cm.WalkBook();
                                break;
                            case 2:
                                
                                cm.Settings();
                                break;
                            case 3:
                                Console.WriteLine("Have a nice day! =)");
                                uow.Dispose();
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
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }*/
    }
}