using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    /// <summary>
    /// Логика ингредиентов.
    /// </summary>
    public class IngredientController
    {
        /// <summary>
        /// Репозиторий ингредиентов.
        /// </summary>
        private IIngredientUnityOfWork _ingredientUnityOfWork;
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public IngredientController(IIngredientUnityOfWork ingredientUnityOfWork)
        {
            _ingredientUnityOfWork = ingredientUnityOfWork;
        }
        /// <summary>
        /// Загрузка списка ингредиентов.
        /// </summary>
        /// <returns>Список ингредиентов.</returns>
        public List<Ingredient> GetIngredients()
        {
            return _ingredientUnityOfWork.IngredientRepository.Get();
        }
        /// <summary>
        /// Сохранение ингредиента.
        /// </summary>
        public void Save()
        {
            _ingredientUnityOfWork.Save();
        }
        /// <summary>
        /// Добавляет ингредиенты и возвращает готовый список ингредиентов.
        /// </summary
        /// <param name="str">Переменная для обработки ответа пользователя.</param>
        /// <param name="result">Переменная для обработки ответа пользователя в формат int.</param>
        public List<int> AddIngredients(string str="", int result=0)
        {
            do
            {
                Console.WriteLine("Введите колличество ингредиентов: ");
                str = Console.ReadLine();
                if (int.TryParse(str, out result))
                {
                    break;
                }
            } while (true);

            var ingredients = new List<int>();

            for (int count = 1; count <= result; count++)
            {
                Console.WriteLine("Введите ингредиент:");
                Console.Write($"{count}. ");
                str = Console.ReadLine();
                if(!GetIngredients().Any(i => i.Name.ToLower() == str.ToLower()))
                {
                    AddIngredient(str);
                }
                ingredients.Add(GetIngredients().First(i => i.Name.ToLower() == str.ToLower()).Id);
            }
            return ingredients;
        }
        /// <summary>
        /// Добавление ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        private void AddIngredient(string nameIngredient)
        {
            foreach (var ingredient in _ingredientUnityOfWork.IngredientRepository.Get())
            {
                if (ingredient.Name.ToLower() == nameIngredient.ToLower())
                {
                    Console.WriteLine("Такой ингредиент уже существует.");
                    return;
                }
            }
            _ingredientUnityOfWork.IngredientRepository.Insert(new Ingredient(GetIngredients().Last().Id+1,nameIngredient));
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public Ingredient FindAndGetIngredient(string nameIngredient)
        {
            var ingredients = _ingredientUnityOfWork.IngredientRepository.Get();

            if(ingredients.Any(ingr => ingr.Name.ToLower() == nameIngredient.ToLower()))
            return ingredients.First(ingr => ingr.Name.ToLower()== nameIngredient.ToLower());
            return null;
        }
        /// <summary>
        /// Метод для отображения ингредиентов.
        /// </summary>
        public void DisplayIngredients()
        {
            foreach (var ingredient in GetIngredients())
            {
                Console.WriteLine($"{ingredient.Id}. {ingredient.Name}.");
            }
            Console.WriteLine("\t\t*enter*");
            Console.ReadLine();
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        public void FindIngredient()
        {
            Console.Clear();
            Console.Write("Введите название ингредиента : ");
            var ingr = FindAndGetIngredient(Console.ReadLine().ToLower());
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
                                AddIngredients();
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