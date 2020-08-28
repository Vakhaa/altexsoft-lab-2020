using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.BL.ClassForText
{
    public static class DeleteStringOrChar
    {
        /// <summary>
        /// Метод удаляет символы или слово с текста.
        /// </summary>
        /// <param name="str">Символ или слово для удаления.</param>
        /// <param name="text">Текст.</param>
        public static void Delete(string str, string[] text)
        {
            if (str.Length == 1) //Symbols
            {
                DeleteSymbol(str,text);
            }
            else //Words
            {
                DeleteWord(str, text);
            }
        }
        /// <summary>
        /// Метод удаляет символы с текста.
        /// </summary>
        /// <param name="deleteSymblol">Символ для удаления.</param>
        /// <param name="text">Текст.</param>
        private static void DeleteSymbol(String deleteSymblol, string[] text)
        {
            int count = 0; //Итератор для foreach.
            bool isExist = false; //Булевая переменная для проверки существования удаляемого обьекта.
            
            foreach (string words in text)
            {
                if (words.Contains(deleteSymblol))
                {
                    isExist = true;
                    text[count] = words.Replace(deleteSymblol, "");
                }
                count++;
            }

            if (!isExist)
            {
                Console.WriteLine($"This file does not have this \"{deleteSymblol}\"");
            }
        }
        /// <summary>
        /// Метод удаляет слова с текста.
        /// </summary>
        /// <param name="deleteWord">Cлово для удаления.</param>
        /// <param name="text">Текст.</param>
        private static  void DeleteWord(String deleteWord, string[] text)
        {
            int count = 0; //Итератор для foreach.
            bool isExist = false; //Булевая переменная для проверки существования удаляемого обьекта.
            
            foreach (string words in text)
            {
                if (words.Contains(deleteWord))
                {
                    isExist = true;
                    text[count] = words.Remove(words.IndexOf(deleteWord), deleteWord.Length) + " ";
                }
                count++;
            }

            if (!isExist)
            {
                Console.WriteLine($"This file does not have \"{deleteWord}\"");
            }
        }
    }
}
