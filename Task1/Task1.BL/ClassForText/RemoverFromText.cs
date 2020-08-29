using System;

namespace Task1.BL.ClassForText
{
    public static class RemoverFromText
    {
        /// <summary>
        /// Метод удаляет символы с текста.
        /// </summary>
        /// <param name="deleteSymblol">Символ для удаления.</param>
        /// <param name="text">Текст.</param>
        public static void Delete(string deleteSymblol, ref TextSaver text)
        {
            int count = 0; //Итератор для foreach.
            bool isExist = false; //Булевая переменная для проверки существования удаляемого обьекта.
            
            foreach (string words in text.Words)
            {
                if (words.Contains(deleteSymblol))
                {
                    isExist = true;
                    text.Words[count] = words.Replace(deleteSymblol, "");
                }
                count++;
            }

            text.Synchro();
            if (!isExist)
            {
                Console.WriteLine($"This file does not have this \"{deleteSymblol}\"");
            }
        }
    }
}
