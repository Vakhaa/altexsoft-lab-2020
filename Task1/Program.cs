using System;
using Task1.BL;

namespace Task1
{

    class Program
    {   
        static void Main(string[] args)
        {
<<<<<<< HEAD
            IFileTxt reader = new FileManager();
            WalkerDirectories wd = new WalkerDirectories();
            ConsolManager cm = new ConsolManager((IWalkerDirectories)wd,reader);
=======
            ConsolManager cm = new ConsolManager();
<<<<<<< HEAD
>>>>>>> parent of dc673c1... Fixed all
            string str;//Строка для обработки ответа пользователя.
            while (true)
            {
                Console.Clear();
<<<<<<< HEAD
                Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full Path: \"fp\", Open File: \"open\", Exit: \"bye\"", PathManager.Path) ; //toolbar
=======
                Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full Path: \"fp\", Open File: \"open\", Exit: \"bye\"", cm.getPath()) ; //toolbar
>>>>>>> parent of dc673c1... Fixed all

                wd.DisplayDirectories();

                wd.DisplayFilesDirectory();

                Console.WriteLine("Next directory :");
                str = Console.ReadLine();

                cm.MenuBar(str);

                wd.SearchDirectories(str);
=======
            while (true)
            {
                cm.Walk(out String str);

                if (ConsolManager.isExite(out str)) return;
>>>>>>> parent of e10b476... Fixed
            }
        }
    }
}