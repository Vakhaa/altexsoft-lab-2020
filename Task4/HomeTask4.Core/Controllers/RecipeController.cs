using System.Collections.Generic;
using System.Linq;
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
        public List<Recipe> GetRecipes()
        {
            _unitOfWork.Repository.ListAsync<IngredientsInRecipe>().Wait();
            _unitOfWork.Repository.ListAsync<StepsInRecipe>().Wait();
            var temp = _unitOfWork.Repository.ListAsync<Recipe>();
            temp.Wait();
            return temp.Result.ToList();
        }
        /// <summary>
        /// Сохранение рецепта.
        /// </summary>
        public void Save()
        {
            _unitOfWork.SaveChangesAsync();
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
            var getRecipes = GetRecipes();
            foreach (var recipe in getRecipes)
            {
                if (recipe.Name == nameRecipe)
                {
                    CurrentRecipe = recipe;
                    return;
                }
            }

            Recipe r = new Recipe( nameRecipe, subcategoriesId, description);
            _unitOfWork.Repository.AddAsync(r).Wait();
            _unitOfWork.SaveChangesAsync().Wait();
            var currentRecipe = GetRecipes().FirstOrDefault(p => p.Name == r.Name);
            for (int steps=0; steps<ingredientsId.Count;steps++)
            {
                _unitOfWork.Repository.AddAsync(new IngredientsInRecipe(currentRecipe.Id, ingredientsId[steps], countIngred[steps])).Wait();
                _unitOfWork.SaveChangesAsync().Wait();
            }
            foreach(var steps in stepsHowCooking)
            {
                _unitOfWork.Repository.AddAsync(new StepsInRecipe(currentRecipe.Id, steps)).Wait();
                _unitOfWork.SaveChangesAsync().Wait();
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
            return GetRecipes().FirstOrDefault(r => r.Id == recipesId);
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
