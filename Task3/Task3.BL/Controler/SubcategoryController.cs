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
        public Subcategory CurrentSubcategories { get; set; }
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
        /// <param name="subcategoriesId">Список индксов подкатегорий активной категории.</param>
        /// <param name="count">Переменная для итерации.</param>
        public void DisplaySubcategory(List<int> subcategoriesId, int count=1)
        {
            var Subcategories = GetSubcategories();
            foreach (var subcategory in subcategoriesId)
            {
                Console.WriteLine($"{count}." +
                    $"{Subcategories.First(c => c.Id == subcategory).Name}");
                count++;
            }
        }
        public void DisplaySubcategory(int categoryId, int count = 1)
        {
            var Subcategories = GetSubcategories().Where(c => c.CategoryId == categoryId);
            foreach (var subcategory in Subcategories)
            {
                Console.WriteLine($"{count}." +
                    $"{subcategory.Name}");
                count++;
            }
        }
        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        public Subcategory AddSubcategory()
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
                if (!subcategories.Any(s => s.Name.ToLower() == str.ToLower()))
                {
                    CurrentSubcategories = new Subcategory(str);
                    _subcategoryUityOfWork.SubcategoryRepository.Insert(CurrentSubcategories);
                    _subcategoryUityOfWork.SubcategoryRepository.Save();
                    return CurrentSubcategories;
                }
                else
                {
                    CurrentSubcategories = subcategories.First(s => s.Name.ToLower() == str.ToLower());
                    Console.WriteLine("Такая подкатегория уже есть.");
                    Console.ReadLine();
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Напишите название подкатегории. \n\t\t*enter*");
                Console.ReadKey();
                return AddSubcategory();
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        /// <param name="subcategoriesId">Индексы подкатегорий активной категории.</param>
        /// <param name="str">Параметр, для обработки ответа пользователя.</param>
        public bool WalkSubcategories(List<int> subcategoriesId, string str = "")
        {
            var subcategories = GetSubcategories();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t*exit: bye, back: back*");
                DisplaySubcategory(subcategoriesId);
                Console.WriteLine("Подкатегория (id):");
                str = Console.ReadLine();
                if (int.TryParse(str, out int result))
                {
                    CurrentSubcategories = subcategories.First(s=>s.Id==subcategoriesId[result-1]);
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
