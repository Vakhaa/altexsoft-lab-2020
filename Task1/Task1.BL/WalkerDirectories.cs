using System;
using System.IO;

namespace Task1.BL
{
    /// <summary>
    /// Класс для работы с директориями и файлами
    /// </summary>
    public class WalkerDirectories
    {
        #region Поля и свойтсва
        /// <summary>
        /// backup Path
        /// </summary>
        private string _tempPath;
        /// <summary>
        /// Местопложение файла
        /// </summary>
        private static string _path;
        /// <summary>
        /// Диски
        /// </summary>
        public static readonly DriveInfo[] Drivers = DriveInfo.GetDrives();
        /// <summary>
        /// Директория
        /// </summary>
        private static string[] _dirs;
        /// <summary>
        /// Инкапсуляция поля _path
        /// </summary>
        public string Path { get { return _path; } }
        #endregion
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public WalkerDirectories()
        {
            ChangeDisk();
            _dirs = Directory.GetDirectories(_path);
        }
        /// <summary>
        /// Метод для смены диска.
        /// </summary>
        public void ChangeDisk()
        {
            String str;//Строковая перемена, для хранения ответа пользователя.
            Console.WriteLine("Choose disk: ");
            foreach (var drive in Drivers)
            {
                Console.Write(drive.Name + " ");
            }
            Console.WriteLine();
            str = Console.ReadLine().ToUpper();
            foreach (var drive in Drivers)
            {
                if (str == drive.Name)
                {
                    if (drive.IsReady)
                    {
                        _path = str;
                        _dirs = Directory.GetDirectories(_path);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Disk is not ready. Try again.");
                        str = "";
                        ChangeDisk();
                        return;
                    }

                }
            }
            Console.WriteLine("Mistake, try again!");
            ChangeDisk();
        }
        /// <summary>
        /// Отображение директории
        /// </summary>
        public void DisplayDirectories()
        {
            Console.WriteLine("Directories :");
            foreach (string nameDir in _dirs)
                Console.WriteLine("\t" + nameDir);
        }
        /// <summary>
        /// Отображение файлов
        /// </summary>
        public void DisplayFiles()
        {
            Console.WriteLine("Files :");
            foreach (string nameFile in Directory.GetFiles(_path))
                Console.WriteLine("\t" + nameFile);
        }
        /// <summary>
        /// Поиск директории
        /// </summary>
        /// <param name="str">Название директории</param>
        public void SearchDirectories(string str)
        {
            foreach (string nameDir in _dirs)
            {
                if (_path + str == nameDir)
                {
                    _tempPath = _path;
                    _path += str + "\\";

                    try
                    {
                        _dirs = Directory.GetDirectories(_path);
                        return;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Access is denied... Try again. \t*enter*");
                        Console.ReadLine();
                        _path = _tempPath;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// Востановление предыдущей директории
        /// </summary>
        public void BackupPath()
        {
            if(string.IsNullOrWhiteSpace(_tempPath))
            {
                throw new ArgumentNullException("Нет сохранения предыдущего место положения в директории", nameof(_tempPath));
            }
            _path = _tempPath;
        }
        /// <summary>
        /// Сетер для пути 
        /// </summary>
        /// <param name="path">Путь</param>
        public void SetPath(string path)
        {
            _path = path;
        }
        /// <summary>
        /// Сохраняет указанный путь для backup-а.
        /// </summary>
        /// <param name="path">Местоположения в директории</param>
        public void SetBackupPath(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Нет сохранения предыдущего место положения в директории", nameof(_tempPath));
            }
            _tempPath = path;
        }
        /// <summary>
        /// Устанавливает директорию, по заданому пути
        /// </summary>
        /// <param name="path">Местоположение директории</param>
        public void SetDireketories(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) 
            throw new ArgumentNullException("Ошибка с получением списка директорий, так как неверный путь", nameof(path));
            _dirs = Directory.GetDirectories(path);
        }
    }
}