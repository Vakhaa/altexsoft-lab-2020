using System;

namespace Task1.BL
{
    /// <summary>
    /// Класс для обработки пути в каталоге.
    /// </summary>
    public static  class PathManager
    {
        #region Поля и свойтсва
        /// <summary>
        /// backup Path
        /// </summary>
        private static string _tempPath;
        /// <summary>
        /// Местопложение файла
        /// </summary>
        private static string _path;
        /// <summary>
        /// Инкапсуляция поля _path
        /// </summary>
        public static string Path { get =>_path; set { 
            if(!string.IsNullOrEmpty(value))
                {
                    _path = value;
                }
                else
                {
                    throw new ArgumentNullException("Путь не может быть Null", nameof(value));
                } 
            } 
        }
        #endregion
        /// <summary>
        /// Востановление предыдущего каталога.
        /// </summary>
        public static void BackupPath()
        {
            if (string.IsNullOrWhiteSpace(_tempPath))
            {
                throw new ArgumentNullException("Нет сохранения предыдущего каталога", nameof(_tempPath));
            }
            _path = _tempPath;
        }
        /// <summary>
        /// Сохраняет указанный путь для backup-а.
        /// </summary>
        /// <param name="path">Местоположения в директории</param>
        public static void SetBackupPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Нельзя сохранить пустой путь", nameof(path));
            }
            _tempPath = path;
        }
    }
}
