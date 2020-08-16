using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика рецептов
    /// </summary>
    public class RecipesControler //TODO проверить методы еще раз, в случае чего убрать комментарии
    {
        /// <summary>
        /// Рецепты
        /// </summary>
        public List<Recipe> Recipes{get;}
        /// <summary>
        /// Активный рецепт
        /// </summary>
        public Recipe CurrentRecipes { get; set; }
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
        private List<Recipe> GetRecipes()
        {
            return JSONReader.DeserialezeFile<Recipe>(Recipes,"rcps.json");
            /*JsonSerializer des = new JsonSerializer();
            
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
            }*/
        }
        /// <summary>
        /// Сохранение рецепта
        /// </summary>
        public void Save()
        {
            JSONReader.Save(Recipes, "rcps.json");
         /*   JsonSerializer serializer = new JsonSerializer();
             //     serializer.Converters.Add(new JavaScriptDateTimeConverter()); //dla daty
            
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"text.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, JsonConvert.SerializeObject(Recipes, Formatting.Indented));
            }*/
        }
        /// <summary>
        /// Добавить рецепт
        /// </summary>
        /// <param name="recipes">Рецепт</param>
        public void AddRecipes(string NameRecipe, string categories, string description, List<string>ingradients,List<string>recipes)
        {
            foreach(var recip in Recipes)
            {
                if(recip.Name==NameRecipe)
                {
                    Console.WriteLine("Такой рецепт уже существует");
                }
            }

            
            Recipe r = new Recipe(NameRecipe, categories, description, ingradients, recipes);
            Recipes.Add(r ?? throw new ArgumentNullException("Нельзя добавить пустой рецепт",nameof(recipes)));
            CurrentRecipes = r; //точно ли нужно делать рецепт активным, возможно он уже будет активным на тот момент так или иначе
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
