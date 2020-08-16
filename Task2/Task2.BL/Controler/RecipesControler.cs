using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика рецептов
    /// </summary>
    public class RecipesControler
    {
        /// <summary>
        /// Рецепты
        /// </summary>
        public List<Recipes> Recipes{get;}
        /// <summary>
        /// Активный рецепт
        /// </summary>
        public Recipes CurrentRecipes { get; set; }
        /// <summary>
        /// Создает контролер моделью рецепта
        /// </summary>
        public RecipesControler()
        {
            Recipes=GetRecipes();
        }
        /// <summary>
        /// Загрузка списка рецепта в приложение
        /// </summary>
        /// <returns>Рецепт</returns>
        private List<Recipes> GetRecipes()
        {
            JsonSerializer des = new JsonSerializer();
            
            if(File.Exists("text.json"))                //читаем файл, если он есть
            {
                using (StreamReader sr = new StreamReader("text.json"))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<List<Recipes>>(des.Deserialize<string>(reader))?? new List<Recipes>();
                        }
                        catch (Exception)
                        {

                            return new List<Recipes>();
                        }
                    }
                }
            }
            else                                           //создаем файл, если его не существует
            {
                Save();
                return new List<Recipes>();
            }
        }
        /// <summary>
        /// Сохранение рецепта
        /// </summary>
        public void Save()
        {

            JsonSerializer serializer = new JsonSerializer();
       //     serializer.Converters.Add(new JavaScriptDateTimeConverter()); //dla daty
            
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"text.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, JsonConvert.SerializeObject(Recipes, Formatting.Indented));
            }
        }
        /// <summary>
        /// Добавить рецепт
        /// </summary>
        /// <param name="recipes">Рецепт</param>
        public void AddRecipes(ref Recipes recipes)
        {
            Recipes.Add(recipes??throw new ArgumentNullException("Нельзя добавить пустой рецепт",nameof(recipes)));
            CurrentRecipes = recipes; //точно ли нужно делать рецепт активным, возможно он уже будет активным на тот момент так или иначе
        }
        /// <summary>
        /// Поиск рецепта
        /// </summary>
        /// <param name="NameRecipes">Название рецепта</param>
        public void FindRecipes(string NameRecipes)
        {
            CurrentRecipes = Recipes.SingleOrDefault(r=>r.Name==NameRecipes);
        }
    }
}
