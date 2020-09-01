using System;
using System.Collections.Generic;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    public class UnitOfWork : ICategoryUnityOfWork, IRecipeUnityOfWork, IIngredientUnityOfWork
    {

        private GenericRepository<List<Category>, Category> _categoryRepository;
        private GenericRepository<List<Recipe>, Recipe> _recipesRepository;
        private GenericRepository<List<Ingredient>, Ingredient> _ingredientRepository;
        private bool disposedValue;

        public GenericRepository<List<Category>, Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository== null)
                {
                    _categoryRepository = new GenericRepository<List<Category>, Category>( "ctgrs.json");
                }
                return _categoryRepository;
            }
        }

        public GenericRepository<List<Recipe>, Recipe> RecipesRepository
        {
            get
            {

                if (_recipesRepository == null)
                {
                    _recipesRepository = new GenericRepository<List<Recipe>, Recipe>("rcps.json");
                }
                return _recipesRepository;
            }
        }

        public GenericRepository<List<Ingredient>, Ingredient> IngredientRepository
        {
            get
            {

                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new GenericRepository<List<Ingredient>, Ingredient>("igrdt.json");
                }
                return _ingredientRepository;
            }
        }

        public void Save(UnitOfWork uow)
        {
            if (uow is ICategoryUnityOfWork)
            {
                if (_categoryRepository != null)
                    _categoryRepository.Save();
            }
            if (uow is IRecipeUnityOfWork)
            {
                if (_recipesRepository != null)
                    _recipesRepository.Save();
            }
            if (uow is IIngredientUnityOfWork)
            {
                if(_ingredientRepository!=null)
                    _ingredientRepository.Save();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_categoryRepository!=null)
                    _categoryRepository.Dispose();
                    if(_ingredientRepository!=null) 
                        _ingredientRepository.Dispose();
                    if(_recipesRepository!=null)
                    _recipesRepository.Dispose();
                }
                _categoryRepository = null;
                _ingredientRepository = null;
                _recipesRepository = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}