using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    public class IngredientController
    {
        private IUnitOfWork _unitOfWork;
        public IngredientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Загрузка списка ингредиентов.
        /// </summary>
        /// <returns>Список ингредиентов.</returns>
        public IEnumerable<Ingredient> GetIngredients()
        {
            return _unitOfWork.Repository.GetWithInclude<Ingredient>(i=>i.IngredientsInRecipe);
        }
        /// <summary>
        /// Добавляет ингредиенты и возвращает его.
        /// </summary
        /// <param name="answer">Переменная для обработки ответа пользователя.</param>
        public async Task<int> AddedIfNewAsync(string answer)
        {
            var ingredients = GetIngredients();
            if (!ingredients.Any(i => i.Name.ToLower() == answer.ToLower()))
            {
               await _unitOfWork.Repository.AddAsync(new Ingredient(answer));
            }
            ingredients = GetIngredients();
            return ingredients.FirstOrDefault(i => i.Name.ToLower() == answer.ToLower()).Id;
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public Ingredient FindAndGetIngredient(string nameIngredient)
        {
            var ingredients = GetIngredients();
            return ingredients.FirstOrDefault(ingr => ingr.Name.ToLower() == nameIngredient.ToLower());
        }
        public Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            return _unitOfWork.Repository.GetByIdAsync<Ingredient>(ingredientId);
        }
    }
}
