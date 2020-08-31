using System;
using System.Collections.Generic;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика ингредиентов.
    /// </summary>
    public class IngradientControler
    {
        /// <summary>
        /// Репозиторий ингредиентов.
        /// </summary>
        private IIngradientRepository _ingradientRepository;
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public IngradientControler(IIngradientRepository ingradientRepository)
        {
            _ingradientRepository = ingradientRepository;
        }
        /// <summary>
        /// Загрузка списка ингрендиентов.
        /// </summary>
        /// <returns>Список нгредиентов.</returns>
        private List<Ingradient> GetIngradients()
        {
            return _ingradientRepository.IngradientRepository.Get();
        }
        /// <summary>
        /// Сохранение ингредиента.
        /// </summary>
        public void Save()
        {
            _ingradientRepository.Save((UnitOfWork)_ingradientRepository);
        }
        /// <summary>
        /// Добавление ингредиентов.
        /// </summary>
        /// <param name="ingradients">Список новых ингредиентов.</param>
        public void AddIngradients(out List<string> ingradients)
        {
            string str;
            int result;
            do
            {
                Console.WriteLine("Введите колличество ингредиентов: ");
                str = Console.ReadLine();
                if (int.TryParse(str, out result))
                {
                    break;
                }
            } while (true);

            ingradients = new List<string>();
            for (int count = 1; count <= result; count++)
            {
                Console.WriteLine("Введите ингредиент:");
                Console.Write($"{count}. ");
                str = Console.ReadLine();
                ingradients.Add(str);
                AddIngradient(str);
                Save();
            }
            Console.ReadLine();
        }
        /// <summary>
        /// Добавление ингрендиента.
        /// </summary>
        /// <param name="nameIngradient">Название ингрендиента.</param>
        private void AddIngradient(string nameIngradient)
        {
            foreach (var ingradient in _ingradientRepository.IngradientRepository.Get())
            {
                if (ingradient.Name == nameIngradient)
                {
                    Console.WriteLine("Такой ингредиент уже существует.");
                    return;
                }
            }
            _ingradientRepository.IngradientRepository.Insert(new Ingradient(nameIngradient) ?? throw new ArgumentNullException("Нельзя добавить пустой ингрендиент.", "ingradient"));
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngradient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public Ingradient FindAndGetIngrandient(string nameIngradient)
        {
            var Ingradients = _ingradientRepository.IngradientRepository;
            for (int id=0; id<Ingradients.Get().Count;id++)
            {
                if(Ingradients.GetByID(id).Name.ToLower()==nameIngradient)
                {
                    return Ingradients.GetByID(id);
                }
            }
            return null;
        }
        /// <summary>
        /// Метод для отображения ингредиентов.
        /// </summary>
        public void DisplayIngradients()
        {
            foreach (var ingradient in GetIngradients())
            {
                Console.WriteLine(ingradient.Name);
            }
            Console.WriteLine("\t\t*enter*");
            Console.ReadLine();
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        public void FindIngradient()
        {
            Console.Clear();
            Console.Write("Введите название ингредиента : ");
            var ingr = FindAndGetIngrandient(Console.ReadLine().ToLower());
            if (ingr != null)
            {
                Console.WriteLine(ingr.Name + " есть в списке.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Такого ингредиента нет, создать ли ?\n" +
                    "1. да\n" +
                    "2. нет\n" +
                    "(number): ");
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                AddIngradients(out List<string> ingradients);
                                return;
                            case 2:
                                return;
                        }
                    }
                }
            }
        }
    }
}