using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using System.Data;
using Newtonsoft.Json.Linq;
//using System.Collections.Generic;

namespace FirstJson
{
    class Program
    {
        public class Product //model
        {
            public DateTime ExpiryDate;
            public decimal Price;
        }

        public static void JustSereliazeProduct()
        {
            Product product = new Product();
            product.ExpiryDate = new DateTime(2008, 12, 28);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"C:\Users\den_v\Desktop\text.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, product);
            }
        }
        public static Product JustDeserilaseProduct()
        {
            JsonSerializer des = new JsonSerializer();
            using (StreamReader sr = new StreamReader(@"C:\Users\den_v\Desktop\text.json"))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JObject FileJson = JObject.FromObject(des.Deserialize(reader));
                JToken result = FileJson;
                return result.ToObject<Product>();
            }
        }

        public static void OdczytPlika(Product p)
        {
            Console.WriteLine(p.ExpiryDate.ToString());
            Console.WriteLine(p.Price.ToString());
        }
        static void Main(string[] args)
        {
            // JustSereliazeProduct();

            OdczytPlika(JustDeserilaseProduct());
        }
    }
}
