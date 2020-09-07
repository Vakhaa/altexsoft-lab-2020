﻿using System;
using Task1.BL.Interfaces;

namespace Task1.BL
{
    /// <summary>
    /// Класс для отображение элементов в консоли
    /// </summary>
    public class ConsolManager
    {
        /// <summary>
        /// Указатель на класс для чтения файла
        /// </summary>
        private Reader _reader;
        /// <summary>
<<<<<<< HEAD
        /// Указатель на класс для работы с текстом
=======
        /// Указатель на класс для работы с текстом.
>>>>>>> parent of dc673c1... Fixed all
        /// </summary>
        private TextWorker _fileText;
        /// <summary>
        /// Указатель на класс для работы с директориями
        /// </summary>
        private WalkerDirectories _walker;
        /// <summary>
        /// Конструтор класса ConsolManager
        /// </summary>
        public ConsolManager()
        {
<<<<<<< HEAD
            _walker = new WalkerDirectories();
=======
            _walker = new WalkerDirectories(); //инициализация папок и файлов
>>>>>>> parent of dc673c1... Fixed all
        }
        

        /// <summary>
        /// Отображение директории.
        /// </summary>
        public void DisplayDirectories()
        {
            _walker.DisplayDirectories();
        }
        /// <summary>
        /// Отображение файлов.
        /// </summary>
        public void DisplayFiles()
        {
            _walker.DisplayFiles();
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
                    for (int i = _walker.Path.Length - 2; i > 0; i--)
                    {
                        if (_walker.Path[i] == '\\')
                        {
                            _walker.SetPath(_walker.Path.Remove(i + 1, _walker.Path.Length - i - 1));
                            _walker.SetDireketories(_walker.Path);
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
                    _walker.SetBackupPath(_walker.Path);
                    //проверка, на тот случай, если пользватель хочет открыть файл указав полный путь
                    if (!str.EndsWith(".txt"))
                    {
                        try
                        {
                            _walker.SetPath(str);
                            _walker.SetDireketories(str);
                            return;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong Path... Try again. \t*enter*");
                            Console.ReadLine();
                            _walker.BackupPath();
                            return;
                        }
                    }
                    else
                    {
                        if(str.Contains("\\")) // проверка на наличие полного пути
                        {
                            if (_reader == null) _reader = new Reader(str);
                            _walker.SetPath(_reader.CutFileName(str)); //обрезаем от пути название файла

                            Console.Clear();
                            OpenFile((IReadTxt)_reader);
                            return;
                        }
                    }
                    break;
                case "open": //Open File
                    Console.Write("File: ");
                    str = Console.ReadLine();
                    Console.WriteLine();
                    Console.Clear();
                    //проверка на тот случай, если пользователь хочет открыть файл указав полный путь
                    if (!str.Contains("\\"))
                    {
                        if (str.EndsWith(".txt"))
                        {
                            if (_reader == null) _reader = new Reader(_walker.Path + str);
                            OpenFile((IReadTxt)_reader);
                            return;
                        }
                    }
                    else
                    {
                        if (str.EndsWith(".txt"))
                        {
                            if (_reader == null) _reader = new Reader(str);

                            _walker.SetPath(_reader.CutFileName(str));
                            Console.Clear();
                            OpenFile((IReadTxt)_reader);
                            return;
                        }
                    }
                    break;
                case "bye": //Exite
                    if (isExite()) Environment.Exit(0);
                    break;
            }
        }
        /// <summary>
        /// Поиск директории.
        /// </summary>
        /// <param name="nameDirectory">Название директории.</param>
        public void SearchDirectories(string nameDirectory)
        {
            _walker.SearchDirectories(nameDirectory);
        }
        /// <summary>
        /// Возвращает путь текущего местположения в директории
        /// </summary>
<<<<<<< HEAD
        /// <returns>Строка</returns>
=======
        /// <returns>Строка.</returns>
>>>>>>> parent of dc673c1... Fixed all
        public string getPath()
        {
            return _walker.Path;
        }
      
        
        /// <summary>
        /// Приватный метод для работы с файлом формата txt.
        /// </summary>
        /// <param name="read">Экземпляр интерфейса IReadTxt.</param>
        private void WorkWithFile(IReadTxt read)
        {
            string str;// Строка, для обработки ответа пользователя.
            while (true)
            {
                Console.WriteLine(_fileText.GetText());
                Console.Write(
                   "1. Delete symbol or word.\n" +
                   "2. Count words and every tenth word.\n" +
                   "3. Backward third sentence.\n" +
                   "4. Close file.\n" +
                   "(number) :"
                   );
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine("Symbol or word: ");
                            _fileText.Delete(Console.ReadLine());
                            break;
                        case 2:
                            _fileText.CountWordsAndTenWords();
                            break;
                        case 3:
                            _fileText.ThirdSentenceReverse();
                            break;
                        case 4:
                            Console.WriteLine("Save changes ?(yes,no)");
                            str = Console.ReadLine().ToLower();
                            if (str == "yes" || str == "y")
                            {
                                read.CreateFile(_fileText.GetText()); //Creat backup
                            }
                            _reader = null; // delete )
                            return;
                        default:
                            break;
                    };
                }
                Console.WriteLine("\t\t *enter*");
                Console.ReadKey();
                Console.Clear();
            }
        }
        /// <summary>
        /// Приватный метод для чтения файла
        /// </summary>
        /// <param name="read">Экземпляр интерфейса IReadTxt.</param>
        private void OpenFile(IReadTxt read)
        {
            _fileText = new TextWorker(read.ReadTxt());

            try
            {
                if (_fileText.Text[0] == "No" && _fileText.Text[1] == "file" && _fileText.Text[4] == "name")
                {
                    Console.WriteLine(_fileText.GetText());
                    _reader = null;
                    Console.ReadKey();
                    return;
                }
                WorkWithFile(read);
            }
            catch(Exception)
            {
                WorkWithFile(read);
            }
        }
        /// <summary>
        /// Булевый метод для закрытия программы.
        /// </summary>
<<<<<<< HEAD
        /// <returns>Булевая переменная - закрывается ли программа</returns>
=======
        /// <returns>Булевая переменная - закрывается ли программа</returns>    
>>>>>>> parent of dc673c1... Fixed all
        private static bool isExite()
        {
            String str;//Обработка ответа пользователя.
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
            return isExite();
        }
    }
}
