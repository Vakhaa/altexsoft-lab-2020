using System;

namespace Task1.BL
{
    /// <summary>
    /// Статический класс для расширения строкового типа
    /// </summary>
    static class StaticString
    {
        /// <summary>
        /// Метод расширения инверсии
        /// </summary>
        /// <param name="str">Сама строка</param>
        /// <param name="startIndex">Индекс начала инверсии</param>
        /// <returns>Строку в инверсии</returns>
        public static string[] Reverse(this String[] str, int startIndex)
        {
            int i = str.Length - 1;
            string[] str1 = new string[i + 1];
            str1.Initialize();
            for (int count = 0; i > 0; count++)
            {
                if (startIndex < count)
                {
                    if (!str[count].Contains('.'))
                    {
                        str1[i] = str[count];
                    }
                    else
                    {
                        str1[i] = str[count].Remove(str[count].IndexOf('.') + 1);
                        return str1;
                    }
                    i--;
                }
            }
            return str1;
        }
    }
}
