using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace Task1.BL.ClassForText
{
    public static class WordsCounter
    {
        /// <summary>
        /// Метод, что считаета слова.
        /// </summary>
        /// <param name="text">Текст.</param>
        public static void CountWords(string text)
        {
            Console.WriteLine("\nCount words: {0}", Regex.Matches(text, @"[\S]+").Count);
        }
    }
}
