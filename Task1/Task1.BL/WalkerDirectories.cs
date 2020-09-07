﻿using System;
using System.IO;

namespace Task1.BL
{
    /// <summary>
    /// Класс для работы с директориями и файлами.
    /// </summary>
<<<<<<< HEAD
    public class WalkerDirectories :IWalkerDirectories
=======
    public class WalkerDirectories
>>>>>>> parent of dc673c1... Fixed all
    {
        #region Поля
        /// <summary>
        /// Диски
        /// </summary>
        public static readonly DriveInfo[] Drivers = DriveInfo.GetDrives();
        /// <summary>
        /// Директория
        /// </summary>
        private static string[] _dirs;
        #endregion
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public WalkerDirectories()
        {
            ChangeDisk(); // Задаем изначально диск, с ктогорого начинаем работу.
            _dirs = Directory.GetDirectories(PathManager.Path); // Теперь можем инициализировать файлы и папки
        }
        /// <summary>
        /// Метод для смены диска.
        /// </summary>
        public void ChangeDisk()
        {
            string str;//Строковая перемена, для хранения ответа пользователя.
            Console.WriteLine("Choose disk: ");
            foreach (var drive in Drivers) // Отображает диски в наличие, так же дисководы и другую перефирию
            {
                Console.Write(drive.Name + " ");
            }
            Console.WriteLine();
            str = Console.ReadLine().ToUpper();
            foreach (var drive in Drivers)
            {
                if (str == drive.Name) 
                {
                    if (drive.IsReady) // Проверяем готов ли диск к использованию
                    {
                        PathManager.Path=str;    // устанавливаем путь
                        _dirs = Directory.GetDirectories(PathManager.Path); // и папки с выбраной директории
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
        /// Отображение директории.
        /// </summary>
        public void DisplayDirectories()
        {
            Console.WriteLine("Directories :");
            foreach (string nameDir in _dirs)
                Console.WriteLine("\t" + nameDir);
        }
        /// <summary>
        /// Отображение файлов директории.
        /// </summary>
        public void DisplayFilesDirectory()
        {
            Console.WriteLine("Files :");
            foreach (string nameFile in Directory.GetFiles(PathManager.Path))
                Console.WriteLine("\t" + nameFile);
        }
        /// <summary>
        /// Поиск директории.
        /// </summary>
        /// <param name="str">Название директории</param>
        public void SearchDirectories(string str)
        {
            foreach (string nameDir in _dirs)
            {
                if (PathManager.Path + str == nameDir)
                {
                    PathManager.SetBackupPath(PathManager.Path); // сохраняем путь, на случай ошибки, что бы вернуться
                    PathManager.Path+=str + "\\"; // задаем новый путь

                    try
                    {
                        _dirs = Directory.GetDirectories(PathManager.Path);
                        return;
                    }
                    catch (UnauthorizedAccessException) // Директория может быть недоступна по уровню доступа
                    {
                        Console.WriteLine("Access is denied... Try again. \t*enter*");
                        Console.ReadLine();
                        PathManager.BackupPath(); //бэкап
                        return;
                    }
                }
            }
        }
        /// <summary>
<<<<<<< HEAD
=======
        /// Востановление предыдущей директории.
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
        /// Сетер для пути директории.
        /// </summary>
        /// <param name="path">Путь директории.</param>
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
>>>>>>> parent of dc673c1... Fixed all
        /// Устанавливает директорию, по заданому пути.
        /// </summary>
        /// <param name="path">Местоположение директории.</param>
        public void SetDirectories(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) 
            throw new ArgumentNullException("Ошибка с получением списка каталогов, так как неверный путь", nameof(path));
            _dirs = Directory.GetDirectories(path);
        }
    }
}