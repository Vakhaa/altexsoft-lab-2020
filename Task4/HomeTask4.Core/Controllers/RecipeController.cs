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
        public  Task<List<Recipe>> GetRecipesAsync()
        {
            return  _unitOfWork.Repository.ListAsync<Recipe>();
        }
        /// <summary>
        /// Добавить новый рецепт.
        /// </summary>
        /// <param name="nameRecipe">Название рецепта.</param>
        /// <param name="subcategoriesId">Индекс подкатегории рецепта.</param>
        /// <param name="description">Описание.</param>
        public async Task CreateRecipeAsync(string nameRecipe, int subcategoriesId, string description)
        {
            var recipes = await GetRecipesAsync();
            foreach (var recipe in recipes)
            {
                if (recipe.Name == nameRecipe)
                {
                    CurrentRecipe = recipe;
                    return;
                }
            }

            Recipe r = new Recipe( nameRecipe, subcategoriesId, description);
            await _unitOfWork.Repository.AddAsync(r);
            recipes = await GetRecipesAsync();
            CurrentRecipe = recipes.FirstOrDefault(p => p.Name == r.Name);
        }
        ///<summary>Добавляет ингредиенты и количество в рецепт.</summary>
        /// <param name="ingredientsId">Индекс ингредиентов.</param>
        /// <param name="countIngred">Количество ингредиентов.</param>
        public async Task AddedIngredientsInRecipeAsync(List<int> ingredientsId, List<string> countIngred)
        {
            for (int steps = 0; steps < ingredientsId.Count; steps++)
            {
                await _unitOfWork.Repository.AddAsync(new IngredientsInRecipe(CurrentRecipe.Id, ingredientsId[steps], countIngred[steps]));
            }
        }
        ///<summary>Добавляет инструкцию приготовления в рецепт.</summary>
        /// <param name="stepsHowCooking">Пошаговая инструкция.</param>
        public async Task AddedStepsInRecipeAsync(List<string> stepsHowCooking)
        {
            foreach (var steps in stepsHowCooking)
            {
                await _unitOfWork.Repository.AddAsync(new StepsInRecipe(CurrentRecipe.Id, steps));
            }
        }
        /// <summary>
        /// Поиск рецепта.
        /// </summary>
        /// <param name="recipesId">Индекс рецепта.</param>
        /// <return>Рецепт.</return>
        public async Task<Recipe> FindRecipeAsync(int recipesId)
        {
            var recipes = await  GetRecipesAsync();
            return recipes.FirstOrDefault(r => r.Id == recipesId);
        }
    }
}
