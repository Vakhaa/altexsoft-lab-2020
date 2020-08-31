using System;
using System.IO;
using Task1.BL.Interfaces;

namespace Task1.BL
{
    /// <summary>
    /// Класс для отображение элементов в консоли.
    /// </summary>
    public class ConsolManager
    {
        /// <summary>
        /// Указатель на класс для работы с файлом.
        /// </summary>
        private IFileTxt _reader;
        /// <summary>
        /// Указатель на класс для работы с директориями.
        /// </summary>
        private IWalkerDirectories _walker;
        /// <summary>
        /// Конструтор класса ConsolManager.
        /// </summary>
        public ConsolManager(IWalkerDirectories walker, IFileTxt reader)
        {
            _walker = walker; //инициализация папок и файлов
            _reader = reader;
        }
        /// <summary>
        /// Метод для обработки команд menu.
        /// </summary>
        /// <param name="str">Обработка ответа пользователя.</param>
        public void MenuBar(string str)
        {
            switch (str.ToLower())
            {
                case ".."://back
                    for (int i = PathManager.Path.Length - 2; i > 0; i--)
                    {
                        if (PathManager.Path[i] == '\\')        // обрезаем последнюю директорию
                        {
                            PathManager.Path= PathManager.Path.Remove(i + 1, PathManager.Path.Length - i - 1);
                            _walker.SetDirectories(PathManager.Path);
                            return;
                        }
                    }
                    break;
                case "cd"://Change disk
                    _walker.ChangeDisk();
                    break;
                case "fp"://Full path
                    Console.Write("Full Path directory: ");
                    str = Console.ReadLine();
                    PathManager.SetBackupPath(PathManager.Path);
                    //проверка, на тот случай, если пользватель хочет открыть файл указав полный путь
                    if (!str.EndsWith(".txt"))
                    {
                        try
                        {
                            PathManager.Path=str;
                            _walker.SetDirectories(str);
                            return;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong Path... Try again. \t*enter*");
                            Console.ReadLine();
                            PathManager.BackupPath();
                            return;
                        }
                    }
                    else
                    {
                        if(File.Exists(str))                        
                        {
                            PathManager.Path=Path.GetDirectoryName(str); //обрезаем от пути название файла
                            Console.Clear();
                            _reader.OpenFile(str);
                            return;
                        }
                    }
                    break;
                case "open": //Open File //Работае иначе от full path
                    Console.Write("File: ");
                    str = Console.ReadLine();
                    Console.WriteLine();
                    Console.Clear();
                    //проверка на тот случай, если пользователь хочет открыть файл указав полный путь
                    if (!str.Contains("\\"))
                    {
                        if (str.EndsWith(".txt"))
                        {
                            _reader.OpenFile(PathManager.Path + str);
                            return;
                        }
                    }
                    else
                    {
                        if (str.EndsWith(".txt"))
                        {
                            PathManager.Path=Path.GetDirectoryName(str)+"\\";
                            Console.Clear();
                            _reader.OpenFile(str);
                            return;
                        }
                    }
                    break;
                case "bye": //Exite
                    if (IsExite()) Environment.Exit(0);
                    break;
            }
        }  

        /// <summary>
        /// Булевый метод для закрытия программы.
        /// </summary>
        /// <returns>Булевая переменная - закрывается ли программа</returns>    
        private static bool IsExite()
        {
            string str;//Обработка ответа пользователя.
            Console.Write("Close console? (yes,no): ");
            str = Console.ReadLine().ToLower();
            if (str == "yes" || str == "y")
            {
                Console.WriteLine("Have a nice day!");
                return true;
            }
            else if (str == "no" || str == "n")
            {
                return false;
            }
            Console.WriteLine("Mistake, try again");
            return IsExite();
        }
    }
}
