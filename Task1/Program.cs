using System;
using Task1.BL;
using Task1.BL.Interfaces;

namespace Task1
{

    class Program
    {
        static void Main(string[] args)
        {
            IWalkerDirectories wd = new WalkerDirectories();
            ConsolManager cm = new ConsolManager(wd);
            string str;//Строка для обработки ответа пользователя.
            while (true)
            {
                Console.Clear();
                Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full Path: \"fp\", Open File: \"open\", Exit: \"bye\"", cm.GetPath()) ; //toolbar

                cm.DisplayDirectories();

                cm.DisplayFiles();

                Console.WriteLine("Next directory :");
                str = Console.ReadLine();

                cm.MenuBar(str);

                cm.SearchDirectories(str);
            }
        }
    }
}