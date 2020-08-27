using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.BL.ClassForText
{
    public static class EveryTenерWord
    {
        /// <summary>
        /// Выводит каждое десятое слово.
        /// </summary>
        /// <param name="text">Текст.</param>
        public static void Words(string[] text)
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
