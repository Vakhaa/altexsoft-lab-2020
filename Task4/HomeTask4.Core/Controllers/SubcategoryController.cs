﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    public class SubcategoryController
    {
        IUnitOfWork _unityOfWork;
        public Category CurrentSubcategory { get; set; }
        private List<int> CurrentSubcategoriesInCategory { get; set; }
        public List<Category> GetSubcategories()
        {
            return _unityOfWork.Repository.ListAsync<Category>().GetAwaiter().GetResult().Where(c => c.ParentId != null).ToList();
        }
        public SubcategoryController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="nameSubcategory">Название новой подкатегории.</param>
        public async Task<Category> AddSubcategoryAsync(int categoryId, string nameSubcategory)
        {
            var subcategories = GetSubcategories();

            if (!int.TryParse(nameSubcategory, out int result))
            {
                if (!subcategories.Any(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId))
                {
                    CurrentSubcategory = new Category(nameSubcategory, categoryId);
                    await _unityOfWork.Repository.AddAsync(CurrentSubcategory);
                    await SaveAsync();
                    return CurrentSubcategory;
                }
                else
                {
                    return CurrentSubcategory = subcategories.FirstOrDefault(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId);
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        /// <param name="str">Параметр, для обработки ответа пользователя.</param>
        public bool WalkSubcategories(string str)
        {
            if (int.TryParse(str, out int result))
            {
                CurrentSubcategory = GetSubcategories().FirstOrDefault(s => s.Id == CurrentSubcategoriesInCategory[result - 1]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddCurrentSubcategoriesInCategory(int id)
        {
            if(CurrentSubcategoriesInCategory is null)
            {
                CurrentSubcategoriesInCategory = new List<int>();
            }
            CurrentSubcategoriesInCategory.Add(id);
        }
        public void ClearCurrentSubcategoriesInCategory()
        {
            CurrentSubcategoriesInCategory = null;
        }
        /// <summary>
        /// Метод возвращает CurrentSubcategoriesInCategory, используеться для тестов скрытого поля.
        /// </summary>
        /// <returns></returns>
        public List<int> GetCurrentSubcategoriesInCategory()
        {
            return CurrentSubcategoriesInCategory;
        }
        public async Task SaveAsync()
        {
           await _unityOfWork.SaveChangesAsync();
        }
    }
}