using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public async Task<List<Category>> GetSubcategoriesAsync()
        {
            return await Task.Run(()=> _unityOfWork.Repository.ListAsync<Category>().GetAwaiter().GetResult().Where(c => c.ParentId != null).ToList());
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
        public Category AddSubcategory(int categoryId, string nameSubcategory)
        {
            var subcategories = GetSubcategoriesAsync().GetAwaiter().GetResult();

            if (!int.TryParse(nameSubcategory, out int result))
            {
                if (!subcategories.Any(s => s.Name.ToLower() == nameSubcategory.ToLower() && s.ParentId == categoryId))
                {
                    CurrentSubcategory = new Category(nameSubcategory, categoryId);
                    _unityOfWork.Repository.AddAsync(CurrentSubcategory).GetAwaiter().GetResult();
                    SaveAsync().GetAwaiter().GetResult();
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
                CurrentSubcategory = GetSubcategoriesAsync().GetAwaiter().GetResult().FirstOrDefault(s => s.Id == CurrentSubcategoriesInCategory[result - 1]);
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
        public async Task SaveAsync()
        {
           await _unityOfWork.SaveChangesAsync();
        }
    }
}
