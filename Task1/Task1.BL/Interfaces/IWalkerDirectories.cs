using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task1.BL.Interfaces
{
    public interface IWalkerDirectories
    {
        /// <summary>
        /// Инкапсуляция поля _path
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// Метод для смены диска.
        /// </summary>
        public void ChangeDisk();
        /// <summary>
        /// Отображение директории.
        /// </summary>
        public void DisplayDirectories();
        /// <summary>
        /// Отображение файлов.
        /// </summary>
        public void DisplayFiles();
        /// <summary>
        /// Поиск директории.
        /// </summary>
        /// <param name="str">Название директории</param>
        public void SearchDirectories(string str);
        /// <summary>
        /// Востановление предыдущей директории.
        /// </summary>
        public void BackupPath();
        /// <summary>
        /// Сетер для пути директории.
        /// </summary>
        /// <param name="path">Путь директории.</param>
        public void SetPath(string path);
        /// <summary>
        /// Сохраняет указанный путь для backup-а.
        /// </summary>
        /// <param name="path">Местоположения в директории</param>
        public void SetBackupPath(string path);
        /// <summary>
        /// Устанавливает директорию, по заданому пути.
        /// </summary>
        /// <param name="path">Местоположение директории.</param>
        public void SetDireketories(string path);
    }
}
