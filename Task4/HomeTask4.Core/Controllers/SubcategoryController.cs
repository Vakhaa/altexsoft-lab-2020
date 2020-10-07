﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var temp = _unityOfWork.Repository.ListAsync<Category>();
            temp.Wait();
            return temp.Result.Where(c => c.ParentId != null).ToList();
        }
        public SubcategoryController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        /// <summary>
        /// Добавления новой подкатегории.
        /// </summary>
        public Category AddSubcategory(int categoryId,string str="")
        {
            var subcategories = GetSubcategories();

            if (!int.TryParse(str, out int result))
            {
                if (!subcategories.Any(s => s.Name.ToLower() == str.ToLower() && s.ParentId == categoryId))
                {
                    CurrentSubcategory = new Category(str, categoryId);
                    _unityOfWork.Repository.AddAsync(CurrentSubcategory);
                    return CurrentSubcategory;
                }
                else
                {
                    CurrentSubcategory = subcategories.First(s => s.Name.ToLower() == str.ToLower() && s.ParentId == categoryId);
                    return null;
                }
            }
            else
            {
                return AddSubcategory(categoryId);
            }
        }
        /// <summary>
        /// Метод для выбора пользователем конкретной подкатегории из списка.
        /// </summary>
        /// <param name="str">Параметр, для обработки ответа пользователя.</param>
        public bool WalkSubcategories(string str = "")
        {
            if (int.TryParse(str, out int result))
            {
                CurrentSubcategory = GetSubcategories().First(s => s.Id == CurrentSubcategoriesInCategory[result - 1]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddCurrentSubcategoriesInCategorr(int id)
        {
            if(CurrentSubcategoriesInCategory is null)
            {
                CurrentSubcategoriesInCategory = new List<int>();
            }
            CurrentSubcategoriesInCategory.Add(id);
        }
        public void ClearCurrentSubcategoriesInCategorr()
        {
            CurrentSubcategoriesInCategory = null;
        }
        public void Save()
        {
            _unityOfWork.SaveChangesAsync();
        }
    }
}
