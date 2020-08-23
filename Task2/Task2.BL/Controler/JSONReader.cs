using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Класс для работы с файлами формата json.
    /// </summary>
    static class JSONReader
    {
        /// <summary>
        /// Загрузка данных.
        /// </summary>
        /// <typeparam name="T">Возвращаемый тип.</typeparam>
        /// <param name="nameFile">Имя файла json<./param>
        /// <returns>Тип T.</returns>
        public static List<T> DeserialezeFile<T>(Object o, String nameFile)
        {
            JsonSerializer des = new JsonSerializer();

            if (File.Exists(nameFile))                           //читаем файл, если он есть
            {
                using (StreamReader sr = new StreamReader(nameFile))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<List<T>>(des.Deserialize<string>(reader));
                        }
                        catch (Exception)
                        {

                            return new List<T>();
                        }
                    }
                }
            }
            else                                           //создаем файл, если его не существует
            {
                Save(o,nameFile);
                return new List<T>();
            }
        }
        /// <summary>
        /// Сохранение данных.
        /// </summary>
        static public void Save(Object o, string nameFile)
        {

            JsonSerializer serializer = new JsonSerializer();

            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(nameFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, JsonConvert.SerializeObject(o, Formatting.Indented));
            }
        }
    }
}
