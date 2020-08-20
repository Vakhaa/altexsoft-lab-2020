using System;
using System.IO;

namespace Task1.BL
{
    /// <summary>
    /// Интерфейс для чтения файлов формата txt.
    /// </summary>
     public interface IReadTxt
    {
        public String ReadTxt();
        public void CreateFile(string text);
    }
    /// <summary>
    /// Класс для чтения файлов.
    /// </summary>
    public class Reader : IReadTxt //IRreadPDF and other
    {
        /// <summary>
        /// Сохранения местоположения файла
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
            this._path = path;
            FileName = path.Substring(path.LastIndexOf('\\')).Trim('\\');
        }
        /// <summary>
        /// Метод для чтения текста в файле формата txt.
        /// </summary>
        /// <returns>Текст</returns>
        public String ReadTxt()
        {
            try
            {
                using (FileStream fstream = File.OpenRead(_path))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    return System.Text.Encoding.Default.GetString(array);
                }
            }
            catch (FileNotFoundException e)
            {
                return "No file with this name";
            }
        }
        /// <summary>
        /// Метод для обрезки от пути названия файла.
        /// </summary>
        /// <param name="_path">Полный путь файла.</param>
        /// <returns>Директорию файла.</returns>
        public string CutFileName(string path)
        {
            for (int i = path.Length - 1; i > 0; i--)  //обрезаем от пути название файла
            {
                if (path[i] != '\\')
                {
                    path = path.TrimEnd(path[i]);
                }
                else
                {
                    return path;
                }
            }
            return path;
        }
        /// <summary>
        /// Создает backup файла.
        /// </summary>
        /// <param name="text">Содержимое файла.</param>
        public void CreateFile(string text)
        {
            using (FileStream fstream = new FileStream(CutFileName(_path) + "~" + FileName, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл ~" + FileName);
            }
        }
    }
}
