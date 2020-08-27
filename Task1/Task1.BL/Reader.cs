using System;
using System.IO;
using Task1.BL.Interfaces;

namespace Task1.BL
{
    /// <summary>
    /// Класс для чтения файлов.
    /// </summary>
    public class Reader : IReadTxt //IRreadPDF and other
    {
        /// <summary>
        /// Сохранения местоположения файла.
        /// </summary>
        private string _path;
        /// <summary>
        /// Название файла.
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="_path">Местоположения файла.</param>
        public Reader(string path)
        {
            _path = path;
            FileName = Path.GetFileName(path);
        }
        /// <summary>
        /// Метод для чтения текста в файле формата txt.
        /// </summary>
        /// <returns>Текст</returns>
        public String ReadTxt()
        {
            try
            {
                return File.ReadAllText(_path);
            }
            catch (FileNotFoundException)
            {
                return "No file with this name";
            }
        }
        /// <summary>
        /// Создает backup файла.
        /// </summary>
        /// <param name="text">Содержимое файла.</param>
        public void CreateFile(string text)
        {
            File.WriteAllText(Path.GetFullPath(_path) + "~" + FileName,text);
            Console.WriteLine("Текст записан в файл ~" + FileName);
            Console.ReadLine();
        }
    }
}