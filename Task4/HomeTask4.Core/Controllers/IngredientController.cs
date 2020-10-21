using System.Collections.Generic;
using System.Linq;
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
        public Task<List<Ingredient>> GetIngredientsAsync()
        {
            return _unitOfWork.Repository.ListAsync<Ingredient>();
        }
        /// <summary>
        /// Добавляет ингредиенты и возвращает его.
        /// </summary
        /// <param name="answer">Переменная для обработки ответа пользователя.</param>
        public async Task<int> AddedIfNewAsync(string answer)
        {
            var ingredients = await GetIngredientsAsync();
            if (!ingredients.Any(i => i.Name.ToLower() == answer.ToLower()))
            {
               var t = await _unitOfWork.Repository.AddAsync(new Ingredient(answer));
            }
            ingredients = await GetIngredientsAsync();
            return ingredients.FirstOrDefault(i => i.Name.ToLower() == answer.ToLower()).Id;
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public async Task<Ingredient> FindAndGetIngredientAsync(string nameIngredient)
        {
            var ingredients = await GetIngredientsAsync();
            return ingredients.FirstOrDefault(ingr => ingr.Name.ToLower() == nameIngredient.ToLower());
        }
    }
}
