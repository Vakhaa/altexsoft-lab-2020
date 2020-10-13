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
            GetIngredientsAsync();
        }
        /// <summary>
        /// Загрузка списка ингредиентов.
        /// </summary>
        /// <returns>Список ингредиентов.</returns>
        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await _unitOfWork.Repository.ListAsync<Ingredient>();
        }
        /// <summary>
        /// Сохранение ингредиента.
        /// </summary>
        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
        /// <summary>
        /// Добавляет ингредиенты и возвращает его.
        /// </summary
        /// <param name="str">Переменная для обработки ответа пользователя.</param>
        public int AddedIfNew(string str)
        {
            if (!GetIngredientsAsync().GetAwaiter().GetResult().Any(i => i.Name.ToLower() == str.ToLower()))
            {
               var t = _unitOfWork.Repository.AddAsync(new Ingredient(str)).GetAwaiter().GetResult();
                SaveAsync().GetAwaiter().GetResult();
            }
            return GetIngredientsAsync().GetAwaiter().GetResult().FirstOrDefault(i => i.Name.ToLower() == str.ToLower()).Id;
        }
        /// <summary>
        /// Поиск ингредиента.
        /// </summary>
        /// <param name="nameIngredient">Название ингредиента.</param>
        /// <returns>Ингредиент.</returns>
        public Ingredient FindAndGetIngredient(string nameIngredient)
        {
            var ingredients = GetIngredientsAsync().GetAwaiter().GetResult();

            if (ingredients.Any(ingr => ingr.Name.ToLower() == nameIngredient.ToLower()))
                return ingredients.FirstOrDefault(ingr => ingr.Name.ToLower() == nameIngredient.ToLower());
            return null;
        }
    }
}
