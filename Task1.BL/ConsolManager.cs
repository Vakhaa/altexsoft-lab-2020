using System;

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
        /// Указатель на класс для работы с текстом
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
            _walker = new WalkerDirectories();
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
                    switch (Int32.Parse(str))
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
                    for (int i = _walker.Path.Length - 2; i > 0; i--)
                    {
                        if (_walker.Path[i] == '\\')
                        {
                            _walker.SetPath(_walker.Path.Remove(i + 1, _walker.Path.Length - i - 1));
                            _walker.SetDireketories(_walker.Path);
                            Walk(out str);
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
                    _walker.SetBackupPath(_walker.Path);
                    //проверка, на тот случай, если пользватель хочет открыть файл указав полный путь
                    if (!str.EndsWith(".txt"))
                    {
                        try
                        {
                            _walker.SetPath(str);
                            _walker.SetDireketories(str);
                            Walk(out str);
                            return;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Wrong Path... Try again. \t*enter*");
                            Console.ReadLine();
                            _walker.BackupPath();
                            Walk(out str);
                            return;
                        }
                    }
                    else
                    {
                        if (_reader == null) _reader = new Reader(str);
                        _walker.SetPath(_reader.CutFileName(str)); //обрезаем от пути название файла

                        Console.Clear();
                        OpenFile((IReadTxt)_reader, out str);
                        return;
                    }
                case "open": //Open File //todo isKorekt
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
                            OpenFile((IReadTxt)_reader, out str);
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
                            OpenFile((IReadTxt)_reader, out str);
                            return;
                        }
                    }
                    break;
                case "bye": //Exite
                    if (isExite(out str)) Environment.Exit(0);
                    break;
            }
        }
        /// <summary>
        /// Булевый метод для закрытия программы.
        /// </summary>
        /// <param name="str">Обработка ответа пользователя.</param>
        /// <returns>Булевая переменная - закрывается ли программа</returns>
        public static bool isExite(out String str)
        {
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
