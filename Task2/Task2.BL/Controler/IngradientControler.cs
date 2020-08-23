using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика ингредиентов.
    /// </summary>
    public class IngradientControler
    {
        /// <summary>
        /// Все ингредиенты.
        /// </summary>
        public List<Ingradient> Ingradients { get; }
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public IngradientControler()
        {
            Ingradients = GetIngradients();
        }
        /// <summary>
        /// Загрузка списка ингрендиентов.
        /// </summary>
        /// <returns>Список нгредиентов.</returns>
        private List<Ingradient> GetIngradients()
        {
            return JSONReader.DeserialezeFile<Ingradient>(Ingradients, "igrdt.json");
        }
        /// <summary>
        /// Сохранение ингрендиента.
        /// </summary>
        public void Save()
        {
            JSONReader.Save(Ingradients, "igrdt.json");
        }
        /// <summary>
        /// Добавление ингрендиента.
        /// </summary>
        /// <param name="ingradient">Ингрендиент.</param>
        public void ADDIngradients(string nameIngradients)
        {
            foreach (var ingradient in Ingradients)
            {
                if (ingradient.Name == nameIngradients)
                {
                    Console.WriteLine("Такой ингредиент уже существует.");
                    return;
                }
            }
            Ingradients.Add(new Ingradient(nameIngradients) ?? throw new ArgumentNullException("Нельзя добавить пустой ингрендиент.", "ingradient"));
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngradients">Название ингредиента.</param>
        /// <returns>Ингрединет</returns>
        public Ingradient FindIngrandients(string nameIngradients)
        {
            return Ingradients.SingleOrDefault(ig => ig.Name == nameIngradients);
        }
    }
}
