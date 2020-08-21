using System;
using Task1.BL;

namespace Task1
{

    class Program
    {
        static void Main(string[] args)
        {
            ConsolManager cm = new ConsolManager();
            string str;//Строка для обработки ответа пользователя.
            while (true)
            {
                Console.Clear();
                Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full Path: \"fp\", Open File: \"open\", Exit: \"bye\"", cm.getPath()) ; //toolbar

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