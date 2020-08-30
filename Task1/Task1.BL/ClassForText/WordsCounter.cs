using System;
using System.Text.RegularExpressions;

namespace Task1.BL.ClassForText
{
    public static class WordsCounter
    {
        /// <summary>
        /// Cчитаета слова.
        /// </summary>
        /// <param name="text">Текст.</param>
        public static void CountWords(string text)
        {
            Console.WriteLine("\nCount words: {0}", Regex.Matches(text, @"[\S]+").Count);
        }
        /// <summary>
        /// Считает каждое десятое слово.
        /// </summary>
        /// <param name="text">Текст.</param>
        public static void CountTenthWord(string[] text)
        {
            int count = 1;//Переменная для подсчета колличества слов
            foreach (string word in text)
            {
                if (count % 10 == 0 && count != 1)
                {
                    Console.Write(word);
                    if (count < text.Length - 9) Console.Write(", ");
                }
                count++;
            }
            Console.WriteLine();
        }
    }
}
