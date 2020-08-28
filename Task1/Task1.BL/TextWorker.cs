using System;


namespace Task1.BL
{
    /// <summary>
    /// Класс для работы с текстом в текстовом файле.
    /// </summary>
    public class TextWorker
    {
        /// <summary>
        /// Содержимое текстового файла, по словам.
        /// </summary>
        public String[] Text { get; }
        /// <summary>
        /// Экзкмпляр класса.
        /// </summary>
        /// <param name="text">Содержимое текстового файла.</param>
        public TextWorker(string text)
        {
            Text = text.Split(new char[] { ' ' });
        }
        /// <summary>
        /// Собирает массив строк обратно в текст.
        /// </summary>
        /// <param name="str">Аргумент для возврата значения</param>
        /// <returns>Текст</returns>
        public string GetText(string str = "")
        {
            foreach (string text in Text)
            {
                if (!(text + " ").Contains("  "))
                {
                    str += text + " ";
                }
            }
            return str;
        }
    }
}