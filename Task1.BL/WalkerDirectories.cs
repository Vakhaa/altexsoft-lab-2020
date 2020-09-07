﻿using System;
using System.IO;

namespace Task1.BL
{
    /// <summary>
    /// Класс для работы с директориями и файлами
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
        /// Конструктор класса
        /// </summary>
        public WalkerDirectories()
        {
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
<<<<<<< HEAD:Task1/Task1.BL/WalkerDirectories.cs
            ChangeDisk(); // Задаем изначально диск, с ктогорого начинаем работу.
            _dirs = Directory.GetDirectories(PathManager.Path); // Теперь можем инициализировать файлы и папки
=======
            ChangeDisk(out String str);
            _dirs = Directory.GetDirectories(_path);
>>>>>>> parent of e10b476... Fixed:Task1.BL/WalkerDirectories.cs
=======
            ChangeDisk();
            _dirs = Directory.GetDirectories(_path);
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs
        }
        /// <summary>
        /// Метод для смены диска.
        /// </summary>
        /// <param name="str">Строковая перемена, для хранения ответа пользователя.</param>
        public void ChangeDisk(out String str)
        {
<<<<<<< HEAD:Task1/Task1.BL/WalkerDirectories.cs
            string str;//Строковая перемена, для хранения ответа пользователя.
=======
>>>>>>> parent of e10b476... Fixed:Task1.BL/WalkerDirectories.cs
            Console.WriteLine("Choose disk: ");
            foreach (var drive in Drivers)
            {
                Console.Write(drive.Name + " ");
            }
            Console.WriteLine();
            str = Console.ReadLine();
            foreach (var drive in Drivers)
            {
                if (str == drive.Name)
                {
                    if (drive.IsReady)
                    {
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
                        PathManager.Path=str;    // устанавливаем путь
                        _dirs = Directory.GetDirectories(PathManager.Path); // и папки с выбраной директории
=======
                        _path = str;
                        _dirs = Directory.GetDirectories(_path);
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Disk is not ready. Try again.");
                        str = "";
                        ChangeDisk(out str);
                        return;
                    }

                }
            }
            Console.WriteLine("Mistake, try again!");
            ChangeDisk(out str);
        }
        /// <summary>
        /// Отображение директории
        /// </summary>
        public void WalkDirectories()
        {
            Console.WriteLine("Directories :");
            foreach (string nameDir in _dirs)
                Console.WriteLine("\t" + nameDir);
        }
        /// <summary>
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
        /// Отображение файлов директории.
=======
        /// Отображение файлов
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs
        /// </summary>
<<<<<<< HEAD:Task1/Task1.BL/WalkerDirectories.cs
        public void DisplayFilesDirectory()
=======
        public void WalkFiles()
>>>>>>> parent of e10b476... Fixed:Task1.BL/WalkerDirectories.cs
        {
            Console.WriteLine("Files :");
            foreach (string nameFile in Directory.GetFiles(PathManager.Path))
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
                if (PathManager.Path + str == nameDir)
                {
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
                    PathManager.SetBackupPath(PathManager.Path); // сохраняем путь, на случай ошибки, что бы вернуться
                    PathManager.Path+=str + "\\"; // задаем новый путь
=======
                    _tempPath = _path;
                    _path += str + "\\";
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs

                    try
                    {
                        _dirs = Directory.GetDirectories(PathManager.Path);
                        return;
                    }
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
<<<<<<< HEAD:Task1/Task1.BL/WalkerDirectories.cs
                    catch (UnauthorizedAccessException) // Директория может быть недоступна по уровню доступа
=======
                    catch (UnauthorizedAccessException e)
>>>>>>> parent of e10b476... Fixed:Task1.BL/WalkerDirectories.cs
                    {
                        Console.WriteLine("Access is denied... Try again. \t*enter*");
                        Console.ReadLine();
                        PathManager.BackupPath(); //бэкап
=======
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Access is denied... Try again. \t*enter*");
                        Console.ReadLine();
                        _path = _tempPath;
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs
                        return;
                    }
                }
            }
        }
        /// <summary>
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
<<<<<<< HEAD
=======
        /// Востановление предыдущей директории.
=======
        /// Востановление предыдущей директории
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs
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
<<<<<<< HEAD:Task1.BL/WalkerDirectories.cs
>>>>>>> parent of dc673c1... Fixed all
        /// Устанавливает директорию, по заданому пути.
        /// </summary>
        /// <param name="path">Местоположение директории.</param>
        public void SetDirectories(string path)
=======
        /// Устанавливает директорию, по заданому пути
        /// </summary>
        /// <param name="path">Местоположение директории</param>
        public void SetDireketories(string path)
>>>>>>> parent of 9b86173... Added comments:Task1/Task1.BL/WalkerDirectories.cs
        {
<<<<<<< HEAD:Task1/Task1.BL/WalkerDirectories.cs
            if (string.IsNullOrWhiteSpace(path)) 
            throw new ArgumentNullException("Ошибка с получением списка каталогов, так как неверный путь", nameof(path));
=======
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("Ошибка с получением списка директорий, так как неверный путь", nameof(path));
>>>>>>> parent of e10b476... Fixed:Task1.BL/WalkerDirectories.cs
            _dirs = Directory.GetDirectories(path);
        }
    }
}