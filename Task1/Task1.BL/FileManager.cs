using System;
using System.IO;
using Task1.BL.ClassForText;
using Task1.BL.Interfaces;

namespace Task1.BL
{
    /// <summary>
    /// Класс для чтения и записи файла.
    /// </summary>
    public class FileManager : IFileTxt
    {
        /// <summary>
        /// Название файла.
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// Метод для считывания файла формата txt.
        /// </summary>
        /// <returns>Данные текстового файла.</returns>
        public string ReadTxt()
        {
            try
            {
                return File.ReadAllText(PathManager.Path+FileName);
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
            File.WriteAllText(PathManager.Path + "~" + FileName, text);
            Console.WriteLine("Текст записан в файл ~" + FileName);
            Console.ReadLine();
        }
        /// <summary>
        /// Метод для чтения файла.
        /// </summary>
        /// <param name="path">Путь файла.</param>
        public void OpenFile(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Путь не может быть Null", nameof(path));
            }
            FileName = Path.GetFileName(path);
            var fileText = new TextSaver(ReadTxt());

            try
            {
                if (fileText.Words[0] == "No" && fileText.Words[1] == "file" && fileText.Words[4] == "name")
                {
                    Console.WriteLine(fileText.Text);
                    Console.ReadKey();
                    return;
                }
                WorkWithFile(ref fileText);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// Приватный метод для работы с файлом.
        /// </summary>
        /// <param name="ts">TextSaver.</param>
        private void WorkWithFile(ref TextSaver ts)
        {
            string str;// Строка, для обработки ответа пользователя.
            while (true)
            {
                Console.WriteLine(ts.Text);
                Console.Write(
                   "1. Delete symbol or word.\n" +
                   "2. Count words.\n" +
                   "3. Every tenth word.\n" +
                   "4. Backward third sentence.\n" +
                   "5. Close file.\n" +
                   "(number) :"
                   );
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine("Symbol or word: ");
                            RemoverFromText.Delete(Console.ReadLine(), ref ts);
                            break;
                        case 2:
                            WordsCounter.CountWords(ts.Text);
                            break;
                        case 3:
                            WordsCounter.CountTenthWord(ts.Words);
                            break;
                        case 4:
                            SentencesReverser.ReverseThirdSentence(ts.Text);
                            break;
                        case 5:
                            Console.WriteLine("Save changes ?(yes,no)");
                            str = Console.ReadLine().ToLower();
                            if (str == "yes" || str == "y")
                            {
                                CreateFile(ts.Text); //Creat backup
                            }
                            return;
                        default:
                            break;
                    }
                }
                Console.WriteLine("\t\t *enter*");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}