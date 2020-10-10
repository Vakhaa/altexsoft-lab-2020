using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    public class RecipeController
    {
        private IUnitOfWork _unitOfWork;
        public Recipe CurrentRecipe { get; set; }
        public RecipeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Загрузка списка рецепта.
        /// </summary>
        /// <returns>Список рецептов.</returns>
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            await _unitOfWork.Repository.ListAsync<IngredientsInRecipe>();
            await _unitOfWork.Repository.ListAsync<StepsInRecipe>();
            return await Task.Run(()=> _unitOfWork.Repository.ListAsync<Recipe>().GetAwaiter().GetResult().ToList());
        }
        /// <summary>
        /// Сохранение рецепта.
        /// </summary>
        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
        /// <summary>
        /// Добавить рецепт.
        /// </summary>
        /// <param name="nameRecipe">Название рецепта.</param>
        /// <param name="subcategoriesId">Индекс подкатегории рецепта.</param>
        /// <param name="description">Описание.</param>
        /// <param name="ingredientsId">Индекс ингредиентов.</param>
        /// <param name="countIngred">Количество ингредиентов.</param>
        /// <param name="stepsHowCooking">Пошаговая инструкция.</param>
        public void AddRecipe(string nameRecipe, int subcategoriesId, string description, List<int> ingredientsId, List<string> countIngred, List<string> stepsHowCooking)
        {
            var getRecipes = GetRecipesAsync().GetAwaiter().GetResult();
            foreach (var recipe in getRecipes)
            {
                if (recipe.Name == nameRecipe)
                {
                    CurrentRecipe = recipe;
                    return;
                }
            }

            Recipe r = new Recipe( nameRecipe, subcategoriesId, description);
            _unitOfWork.Repository.AddAsync(r).GetAwaiter().GetResult();
            SaveAsync().GetAwaiter().GetResult();
            var currentRecipe = GetRecipesAsync().GetAwaiter().GetResult().FirstOrDefault(p => p.Name == r.Name);
            for (int steps=0; steps<ingredientsId.Count;steps++)
            {
                _unitOfWork.Repository.AddAsync(new IngredientsInRecipe(currentRecipe.Id, ingredientsId[steps], countIngred[steps])).GetAwaiter().GetResult();
                SaveAsync().GetAwaiter().GetResult();
            }
            foreach (var steps in stepsHowCooking)
            {
                _unitOfWork.Repository.AddAsync(new StepsInRecipe(currentRecipe.Id, steps)).GetAwaiter().GetResult();
                SaveAsync().GetAwaiter().GetResult();
            }
            CurrentRecipe = currentRecipe;
        }
        /// <summary>
        /// Поиск рецепта.
        /// </summary>
        /// <param name="recipesId">Индекс рецепта.</param>
        /// <return>Рецепт.</return>
        public Recipe FindRecipe(int recipesId)
        {
            return GetRecipesAsync().GetAwaiter().GetResult().FirstOrDefault(r => r.Id == recipesId);
        }
        
        /// <summary>
        /// Метод для выбора пользователем конкретного рецепта из списка.
        /// </summary>
        /// <param name="listRecipes">Список рецептов активной подкатегории.</param>
        /// <param name="str">Переменная для обработки ответа пользователя.</param>
        public bool WalkRecipes(List<Recipe> listRecipes, string str)
        {
            if (int.TryParse(str, out int result))
            {
                CurrentRecipe = listRecipes[result - 1];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
