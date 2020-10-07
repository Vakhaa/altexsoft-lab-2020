using System.Collections.Generic;
using System.Linq;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    /// <summary>
    /// Логика ингредиентов.
    /// </summary>
    public class IngredientController
    {
        /// <summary>
        /// Репозиторий ингредиентов.
        /// </summary>
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public IngredientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            GetIngredients();
        }
        /// <summary>
        /// Загрузка списка ингредиентов.
        /// </summary>
        /// <returns>Список ингредиентов.</returns>
        public List<Ingredient> GetIngredients()
        {
            var temp = _unitOfWork.Repository.ListAsync<Ingredient>();
            temp.Wait();
            return temp.Result.OrderBy(i => i.Id).ToList();
        }
        /// <summary>
        /// Сохранение ингредиента.
        /// </summary>
        public void Save()
        {
            _unitOfWork.SaveChangesAsync();
        }
        /// <summary>
        /// Добавляет ингредиенты и возвращает его.
        /// </summary
        /// <param name="str">Переменная для обработки ответа пользователя.</param>
        public int AddedIfNew(string str)
        {
            if (!GetIngredients().Any(i => i.Name.ToLower() == str.ToLower()))
            {
                AddIngredient(str);
            }
            return GetIngredients().First(i => i.Name.ToLower() == str.ToLower()).Id;
        }
        /// <summary>
        /// Добавление ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        private void AddIngredient(string nameIngredient)
        {
            foreach (var ingredient in GetIngredients())
            {
                if (ingredient.Name.ToLower() == nameIngredient.ToLower())
                {
                    return;
                }
            }
            _unitOfWork.Repository.AddAsync(new Ingredient(nameIngredient)).Wait();
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public Ingredient FindAndGetIngredient(string nameIngredient)
        {
            var ingredients = GetIngredients();

            if (ingredients.Any(ingr => ingr.Name.ToLower() == nameIngredient.ToLower()))
                return ingredients.First(ingr => ingr.Name.ToLower() == nameIngredient.ToLower());
            return null;
        }
    }
}
