using System;
using System.Collections.Generic;
using Task2.BL.Interfaces;
using Task2.BL.Model;

namespace Task2.BL.Controler
{
    public class UnitOfWork : ICategoryRepository,IRecipeRepository, IIngradientRepository, IDisposable
    {

        private GenericRepository<List<Category>, Category> _categoryRepository;
        private GenericRepository<List<Recipe>, Recipe> _recipesRepository;
        private GenericRepository<List<Ingradient>, Ingradient> _ingradientRepository;

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

        public GenericRepository<List<Ingradient>, Ingradient> IngradientRepository
        {
            get
            {

                if (_ingradientRepository == null)
                {
                    _ingradientRepository = new GenericRepository<List<Ingradient>, Ingradient>("igrdt.json");
                }
                return _ingradientRepository;
            }
        }

        public void Save(UnitOfWork uow)
        {
            if(uow is ICategoryRepository)
                _categoryRepository.Save();
            if (uow is IRecipeRepository)
                _recipesRepository.Save();
            if (uow is IIngradientRepository)
            _ingradientRepository.Save();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}