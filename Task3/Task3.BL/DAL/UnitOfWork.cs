using System;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    public class UnitOfWork : ICategoryUnityOfWork, IRecipeUnityOfWork, IIngredientUnityOfWork, ISubcategoryUnityOfWork
    {

        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Recipe> _recipesRepository;
        private GenericRepository<Ingredient> _ingredientRepository;
        private GenericRepository<Subcategory> _subcategoryRepository;
        private bool disposedValue;

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository== null)
                {
                    _categoryRepository = new GenericRepository<Category>();
                }
                return _categoryRepository;
            }
        }

        public GenericRepository<Recipe> RecipesRepository
        {
            get
            {

                if (_recipesRepository == null)
                {
                    _recipesRepository = new GenericRepository<Recipe>();
                }
                return _recipesRepository;
            }
        }

        public GenericRepository<Ingredient> IngredientRepository
        {
            get
            {

                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new GenericRepository< Ingredient>();
                }
                return _ingredientRepository;
            }
        }

        public GenericRepository<Subcategory> SubcategoryRepository
        {
            get
            {

                if (_subcategoryRepository == null)
                {
                    _subcategoryRepository = new GenericRepository<Subcategory>();
                }
                return _subcategoryRepository;
            }
        }

        public void Save()
        {
            if (_categoryRepository != null)
                _categoryRepository.Save();
            if (_subcategoryRepository != null)
                _subcategoryRepository.Save();
            if (_recipesRepository != null)
                _recipesRepository.Save();
            if (_ingredientRepository != null)
                _ingredientRepository.Save();
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