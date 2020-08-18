using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика ингредиентов
    /// </summary>
    public class IngradientControler
    {
        /// <summary>
        /// Все ингредиенты
        /// </summary>
        public List<Ingradient> Ingradients { get; }
        public IngradientControler()
        {
            Ingradients = GetIngradients();
        }
        /// <summary>
        /// Загрузка списка ингрендиентов
        /// </summary>
        /// <returns>Ингредиенты</returns>
        private List<Ingradient> GetIngradients()
        {
            return JSONReader.DeserialezeFile<Ingradient>(Ingradients, "igrdt.json");
        }
        /// <summary>
        /// Сохранение ингрендиента
        /// </summary>
        public void Save()
        {
            JSONReader.Save(Ingradients, "igrdt.json");
        }
        /// <summary>
        /// Добавление ингрендиента
        /// </summary>
        /// <param name="ingradient">Ингрендиент</param>
        public void ADDIngradients(string NameIngradients)
        {
            foreach (var ingradient in Ingradients)
            {
                if (ingradient.Name == NameIngradients)
                {
                    Console.WriteLine("Такой ингредиент уже существует");
                    return;
                }
            }
            Ingradients.Add(new Ingradient(NameIngradients) ?? throw new ArgumentNullException("Нельзя добавить пустой ингрендиент", "ingradient"));
        }
        /// <summary>
        /// Поиск ингредиента
        /// </summary>
        /// <param name="NameIngradients">Название ингредиента</param>
        /// <returns></returns>
        public Ingradient FindIngrandients(string NameIngradients)
        {
            return Ingradients.SingleOrDefault(ig => ig.Name == NameIngradients);
        }

    }
}
