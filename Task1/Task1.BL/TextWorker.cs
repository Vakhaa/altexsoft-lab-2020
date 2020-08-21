using System;
using System.Text.RegularExpressions;


namespace Task1.BL
{
    /// <summary>
    /// Класс для работы с текстом в текстовом файле
    /// </summary>
    public class TextWorker
    {
        /// <summary>
        /// Содержимое текстового файла, по словам
        /// </summary>
        public String[] Text { get; }
        /// <summary>
        /// Экзкмпляр класса 
        /// </summary>
        /// <param name="text">Содержимое текстового файла</param>
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
        /// <summary>
        /// Метод удаляет символы или слова с текстаю
        /// </summary>
        /// <param name="str">Обьект для удаления</param>
        /// <param name="count">Итератор для foreach</param>
        /// <param name="isExist">Булевая переменная для проверки существования удаляемого обьекта</param>
        public void Delete(String str, int count = 0, bool isExist = false)
        {
            if (str.Length == 1) //Symbols
            {
                foreach (string words in Text)
                {
                    if (words.Contains(str))
                    {
                        isExist = true;
                        Text[count] = words.Replace(str, "");
                    }
                    count++;
                }
                if (!isExist)
                {
                    Console.WriteLine("This file does not have this \"{0}\"", str);
                }
                return;
            }
            foreach (string words in Text) //words
            {
                if (words.Contains(str))
                {
                    isExist = true;
                    Text[count] = words.Remove(words.IndexOf(str), str.Length) + " ";
                }
                count++;
            }

            if (!isExist)
            {
                Console.WriteLine("This file does not have \"{0}\"", str);
            }
        }
        /// <summary>
        /// Метод, что считаета слова и выводит каждое деятое слово.
        /// </summary>
        /// <param name="count">Переменная для подсчета колличества слов</param>
        public void CountWordsAndTenWords(int count = 1)
        {
            Console.WriteLine("\nCount words: {0}", Regex.Matches(GetText(), @"[\S]+").Count);
            foreach (string word in Text)
            {
                if (count % 10 == 0 && count != 1)
                {
                    Console.Write(word);
                    if (count < Text.Length - 9) Console.Write(", ");
                }
                count++;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Метод выводит третье предложение с конца до начала.
        /// </summary>
        /// <param name="count">Необязательный параметр, для подсчета предложения.</param>
        public void ThirdSentenceReverse(int count = 0)
        {
            Console.WriteLine("\nReverse: ");
            var text = GetText();
            for (int i = 0,temp=0; i < text.Length - 1; i++)
            {
                if (text[i]=='.')
                {
                    count++;
                }
                if (count == 2&&temp==0)
                {
                    temp = i+1;
                }
                if(count==3)
                {
                    var tempchararray = text.ToCharArray(temp,i-temp);
                    Array.Reverse(tempchararray, 0, tempchararray.Length);
                    Console.WriteLine(tempchararray);
                    Console.WriteLine();
                    return;
                }
            }
        }
    }
}