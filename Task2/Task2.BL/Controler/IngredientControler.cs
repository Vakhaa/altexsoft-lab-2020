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
    public class IngredientControler
    {
        /// <summary>
        /// Репозиторий ингредиентов.
        /// </summary>
        private IIngredientUnityOfWork _ingredientUnityOfWork;
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public IngredientControler(IIngredientUnityOfWork ingredientUnityOfWork)
        {
            _ingredientUnityOfWork = ingredientUnityOfWork;
        }
        /// <summary>
        /// Загрузка списка ингрендиентов.
        /// </summary>
        /// <returns>Список нгредиентов.</returns>
        private List<Ingredient> GetIngredients()
        {
            return _ingredientUnityOfWork.IngredientRepository.Get();
        }
        /// <summary>
        /// Сохранение ингредиента.
        /// </summary>
        public void Save()
        {
            _ingredientUnityOfWork.Save((UnitOfWork)_ingredientUnityOfWork);
        }
        /// <summary>
        /// Добавляет ингредиенты и возвращает готовый список ингредиентов.
        /// </summary
        /// <param name="ingredients">Список новых ингредиентов.</param>
        public List<string> AddIngredients()
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

            var ingredients = new List<string>();

            for (int count = 1; count <= result; count++)
            {
                Console.WriteLine("Введите ингредиент:");
                Console.Write($"{count}. ");
                str = Console.ReadLine();
                ingredients.Add(str);
                AddIngredient(str);
                Save();
            }
            Console.ReadLine();
            return ingredients;
        }
        /// <summary>
        /// Добавление ингрендиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингрендиента.</param>
        private void AddIngredient(string nameIngredient)
        {
            foreach (var ingredient in _ingredientUnityOfWork.IngredientRepository.Get())
            {
                if (ingredient.Name == nameIngredient)
                {
                    Console.WriteLine("Такой ингредиент уже существует.");
                    return;
                }
            }
            _ingredientUnityOfWork.IngredientRepository.Insert(new Ingredient(nameIngredient));
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public Ingredient FindAndGetIngredient(string nameIngredient)
        {
            var ingredients = _ingredientUnityOfWork.IngredientRepository.Get();
            if(ingredients.Any(ingr => ingr.Name.ToLower() == nameIngredient))
            return ingredients.First(ingr => ingr.Name.ToLower()==nameIngredient);
            return null;
            
        }
        /// <summary>
        /// Метод для отображения ингредиентов.
        /// </summary>
        public void DisplayIngredients()
        {
            foreach (var ingredient in GetIngredients())
            {
                Console.WriteLine(ingredient.Name);
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