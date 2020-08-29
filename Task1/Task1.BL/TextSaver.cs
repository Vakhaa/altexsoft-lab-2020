namespace Task1.BL
{
    /// <summary>
    /// Класс для работы с текстом в текстовом файле.
    /// </summary>
    public class TextSaver
    {
        /// <summary>
        /// Содержимое текстового файла, по словам.
        /// </summary>
        public string[] Words
        {
            get;
            set;
        }
        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Экзкмпляр класса.
        /// </summary>
        /// <param name="text">Содержимое текстового файла.</param>
        public TextSaver(string text)
        {
            Words = text.Split(new char[] { ' ' });
            Text = text;
        }
        /// <summary>
        /// Синхронизация свойств.
        /// </summary>
        public void Synchro()
        {
            Text = "";
            foreach (var str in Words)
            {
                Text += str + " ";
            }
        }
    }
}