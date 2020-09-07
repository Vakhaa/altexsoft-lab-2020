using System;
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
<<<<<<< HEAD
using System.IO;
=======
>>>>>>> parent of dc673c1... Fixed all
using Task1.BL.Interfaces;
=======
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs

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
<<<<<<< HEAD
        private IFileTxt _reader;
=======
        private Reader _reader;
        /// <summary>
        /// Указатель на класс для работы с текстом.
        /// </summary>
        private TextWorker _fileText;
>>>>>>> parent of dc673c1... Fixed all
        /// <summary>
        /// Указатель на класс для работы с директориями.
        /// </summary>
        private WalkerDirectories _walker;
        /// <summary>
        /// Конструтор класса ConsolManager.
        /// </summary>
<<<<<<< HEAD
        public ConsolManager(IWalkerDirectories walker, IFileTxt reader)
        {
            _walker = walker; //инициализация папок и файлов
            _reader = reader;
=======
        public ConsolManager()
        {
            _walker = new WalkerDirectories(); //инициализация папок и файлов
        }
        /// <summary>
        /// Приватный метод для работы с файлом формата txt.
        /// </summary>
        /// <param name="read">Экземпляр интерфейса IReadTxt.</param>
        /// <param name="str">Строка, для обработки ответа пользователя.</param>
        private void WorkWithFile(IReadTxt read, out string str)
        {
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
                            str = Console.ReadLine();
                            if (str == "yes" | str == "y" | str == "Y" | str == "Yes" | str == "YES")
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
        /// <param name="str">Строка, для обработки ответа пользователя.</param>
        private void OpenFile(IReadTxt read, out string str)
        {
            _fileText = new TextWorker(read.ReadTxt());

            if (_fileText.Text[0] == "No" & _fileText.Text[1] == "file" & _fileText.Text[4] == "name")
            {
                Console.WriteLine(_fileText.GetText());
                _reader = null; Console.ReadKey(); str = ""; return;
            }
            WorkWithFile(read, out str);
        }
        /// <summary>
        /// Метод отображения всех элементов в консоли.
        /// </summary>
        /// <param name="str">Строка для обработки ответа пользователя.</param>
        public void Walk(out String str)
        {
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
            _walker.DisplayFiles();
>>>>>>> parent of dc673c1... Fixed all
=======
            while (true)
            {
                Console.Clear();
                Console.WriteLine("{0} \t| Change disk: \"cd\", Back: \"..\", Full Path: \"fp\", Open File: \"open\", Exit: \"bye\"", _walker.Path); //toolbar

                _walker.WalkDirectories();

                _walker.WalkFiles();

                Console.WriteLine("Next directory :");
                str = Console.ReadLine();

                MenuBar(str);

                _walker.SearchDirectories(str);
                Walk(out str);
            }
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
        }
        /// <summary>
        /// Метод для обработки команд menu.
        /// </summary>
        /// <param name="str">Обработка ответа пользователя.</param>
        public void MenuBar(string str)
        {
            switch (str)
            {
                case ".."://back
                    for (int i = PathManager.Path.Length - 2; i > 0; i--)
                    {
                        if (PathManager.Path[i] == '\\')        // обрезаем последнюю директорию
                        {
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
                            PathManager.Path= PathManager.Path.Remove(i + 1, PathManager.Path.Length - i - 1);
                            _walker.SetDirectories(PathManager.Path);
=======
                            _walker.SetPath(_walker.Path.Remove(i + 1, _walker.Path.Length - i - 1));
                            _walker.SetDireketories(_walker.Path);
                            Walk(out str);
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
                            return;
                        }
                    }
                    break;
                case "cd"://Change disk
                    _walker.ChangeDisk(out str);
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
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
                            PathManager.Path=str;
                            _walker.SetDirectories(str);
=======
                            _walker.SetPath(str);
                            _walker.SetDireketories(str);
                            Walk(out str);
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
                            return;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Wrong Path... Try again. \t*enter*");
                            Console.ReadLine();
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
                            PathManager.BackupPath();
=======
                            _walker.BackupPath();
                            Walk(out str);
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
                            return;
                        }
                    }
                    else
                    {
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
<<<<<<< HEAD
                        if(File.Exists(str))                        
                        {
                            PathManager.Path=Path.GetDirectoryName(str); //обрезаем от пути название файла
=======
                        if(str.Contains("\\")) // проверка на наличие полного пути
                        {
                            if (_reader == null) _reader = new Reader(str);
                            _walker.SetPath(_reader.CutFileName(str)); //обрезаем от пути название файла

>>>>>>> parent of dc673c1... Fixed all
                            Console.Clear();
                            _reader.OpenFile(str);
                            return;
                        }
                    }
                    break;
                case "open": //Open File //Работае иначе от full path
=======
                        if (_reader == null) _reader = new Reader(str);
                        _walker.SetPath(_reader.CutFileName(str)); //обрезаем от пути название файла

                        Console.Clear();
                        OpenFile((IReadTxt)_reader, out str);
                        return;
                    }
                case "open": //Open File
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
                    Console.Write("File: ");
                    str = Console.ReadLine();
                    Console.WriteLine();
                    Console.Clear();
                    //проверка на тот случай, если пользователь хочет открыть файл указав полный путь
                    if (!str.Contains("\\"))
                    {
                        if (str.EndsWith(".txt"))
                        {
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
                            _reader.OpenFile(PathManager.Path + str);
=======
                            if (_reader == null) _reader = new Reader(_walker.Path + str);
                            OpenFile((IReadTxt)_reader, out str);
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
                            return;
                        }
                    }
                    else
                    {
                        if (str.EndsWith(".txt"))
                        {
<<<<<<< HEAD
                            PathManager.Path=Path.GetDirectoryName(str)+"\\";
=======
                            if (_reader == null) _reader = new Reader(str);

                            _walker.SetPath(_reader.CutFileName(str));
>>>>>>> parent of dc673c1... Fixed all
                            Console.Clear();
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
                            _reader.OpenFile(str);
=======
                            OpenFile((IReadTxt)_reader, out str);
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
                            return;
                        }
                    }
                    break;
                case "bye": //Exite
                    if (isExite(out str)) Environment.Exit(0);
                    break;
            }
<<<<<<< HEAD
        }  
=======
        }
        /// <summary>
<<<<<<< HEAD:Task1/Task1.BL/ConsolManager.cs
        /// Поиск директории.
        /// </summary>
        /// <param name="nameDirectory">Название директории.</param>
        public void SearchDirectories(string nameDirectory)
        {
            _walker.SearchDirectories(nameDirectory);
        }
        /// <summary>
        /// Возвращает путь текущего местположения в директории.
        /// </summary>
        /// <returns>Строка.</returns>
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
        /// Приватный метод для чтения файла.
        /// </summary>
        /// <param name="read">Экземпляр интерфейса IReadTxt.</param>
        private void OpenFile(IReadTxt read)
        {
            _fileText = new TextWorker(read.ReadTxt());
>>>>>>> parent of dc673c1... Fixed all

        /// <summary>
        /// Булевый метод для закрытия программы.
        /// </summary>
        /// <returns>Булевая переменная - закрывается ли программа</returns>    
        private static bool isExite()
        {
            string str;//Обработка ответа пользователя.
=======
        /// Булевый метод для закрытия программы.
        /// </summary>
        /// <param name="str">Обработка ответа пользователя.</param>
        /// <returns>Булевая переменная - закрывается ли программа</returns>
        public static bool isExite(out String str)
        {
>>>>>>> parent of e10b476... Fixed:Task1.BL/ConsolManager.cs
            Console.Write("Close console? (yes,no): ");
            str = Console.ReadLine();
            if (str == "yes" | str == "Yes" | str == "y" | str == "Y")
            {
                Console.WriteLine("Have a nice day!");
                return true;
            }
            else if (str == "no" | str == "No" | str == "n" | str == "N")
            {
                return false;
            }
            Console.WriteLine("Mistake, try again");
            return isExite(out str);
        }
    }
}
