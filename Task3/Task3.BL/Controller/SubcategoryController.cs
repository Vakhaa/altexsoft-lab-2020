using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    public class SubcategoryController
    {
        ISubcategoryUnityOfWork _subcategoryUityOfWork;
        public Subcategory CurrentSubcategory { get; set; }
        private List<int> CurrentSubcategoriesInCategory { get; set; } 
        public List<Subcategory> GetSubcategories()
        {
            return _subcategoryUityOfWork.SubcategoryRepository.Get();
        }
        public SubcategoryController(ISubcategoryUnityOfWork subcategoryUnityOfWork)
        {
            _subcategoryUityOfWork = subcategoryUnityOfWork;
        }
        /// <summary>
        /// Метод для отображения подкатегорий.
        /// </summary>
        /// <param name="categoryId">Id активной категории.</param>
        /// <param name="count">Переменная для итерации.</param>
        public void DisplaySubcategory(int categoryId, int count = 1)
        {
            CurrentSubcategoriesInCategory = new List<int>();
            var Subcategories = GetSubcategories().Where(c => c.CategoryId == categoryId);
            foreach (var subcategory in Subcategories)
            {
                CurrentSubcategoriesInCategory.Add(subcategory.Id);
                Console.WriteLine($"{count}." +$" {subcategory.Name}");
                count++;
            }
        }
        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        public Subcategory AddSubcategory(int categoryId)
        {
            Console.WriteLine("Ввидите название подкатегории блюда (Украинская кухня): ");
            var str = Console.ReadLine();
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Нужно задать название", "Subcategory");
            }

            var subcategories = GetSubcategories();

            if(!int.TryParse(str,out int result))
            {
                if (!subcategories.Any(s => s.Name.ToLower() == str.ToLower()&& s.CategoryId== categoryId))
                {
                    CurrentSubcategory = new Subcategory(GetSubcategories().Last().Id+1,str, categoryId);
                    _subcategoryUityOfWork.SubcategoryRepository.Insert(CurrentSubcategory);
                    return CurrentSubcategory;
                }
                else
                {
                    CurrentSubcategory = subcategories.First(s => s.Name.ToLower() == str.ToLower() && s.CategoryId == categoryId);
                    Console.WriteLine("Такая подкатегория уже есть.");
                    return CurrentSubcategory;
                }
            }
            else
            {
                Console.WriteLine("Напишите название подкатегории. \n\t\t*enter*");
                Console.ReadKey();
                return AddSubcategory(categoryId);
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        /// <param name="categoryId">Id активной категории.</param>
        /// <param name="str">Параметр, для обработки ответа пользователя.</param>
        public bool WalkSubcategories(int categoryId, string str = "")
        {
            var subcategories = GetSubcategories();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplaySubcategory(categoryId);
                Console.WriteLine("Подкатегория (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    CurrentSubcategory = subcategories.First(s=>s.Id==CurrentSubcategoriesInCategory[result - 1]);
                    return false;
                }
                else
                {
                    if (ConsoleManager.IsExit(str))
                    {
                        return true;
                    }
                }
            }
        }
    }
}
