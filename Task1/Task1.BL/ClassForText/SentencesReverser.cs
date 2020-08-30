using System;

namespace Task1.BL.ClassForText
{
    public static class SentencesReverser
    {
        /// <summary>
        /// Метод выводит третье предложение с конца до начала.
        /// </summary>
        /// <param name="text">Текст.</param>
        public static void ReverseThirdSentence(string text)
        {
            int count = 0;//параметр, для подсчета предложений
            Console.WriteLine("\nReverse: ");
            for (int i = 0, temp = 0; i < text.Length - 1; i++) //Ищем третье предложения
            {
                if (text[i] == '.')
                {
                    count++;
                }
                if (count == 2 && temp == 0) //Сохраняем индекс после 2 точки, это начало третьего предложения
                {
                    temp = i + 1;
                }
                if (count == 3)
                {
                    var tempchararray = text.ToCharArray(temp, i - temp); // копируем 3 предложение указав начало первого индекса и длину
                    Array.Reverse(tempchararray, 0, tempchararray.Length);
                    Console.WriteLine(tempchararray);
                    Console.WriteLine();
                    return;
                }
            }
        }
    }
}
