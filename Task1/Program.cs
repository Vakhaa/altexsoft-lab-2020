using System;
using Task1.BL;
using Task1.BL.Interfaces;

namespace Task1
{

    class Program
    {
        static void Main(string[] args)
        {
            IFileTxt reader = new FileManager();
            WalkerDirectories wd = new WalkerDirectories();
            ConsolManager cm = new ConsolManager((IWalkerDirectories)wd,reader);
            string str;//Строка для обработки ответа пользователя.
            while (true)
            {
                Console.Clear();
                Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full Path: \"fp\", Open File: \"open\", Exit: \"bye\"", PathManager.Path) ; //toolbar

                wd.DisplayDirectories();

                wd.DisplayFilesDirectory();

                Console.WriteLine("Next directory :");
                str = Console.ReadLine();

                cm.MenuBar(str);

                wd.SearchDirectories(str);
            }
        }
    }
}