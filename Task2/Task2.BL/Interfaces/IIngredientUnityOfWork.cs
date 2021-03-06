﻿using System;
using System.Collections.Generic;
using Task2.BL.Controler;
using Task2.BL.Model;

namespace Task2.BL.Interfaces
{
    public interface IIngredientUnityOfWork:IDisposable
    {
        GenericRepository<List<Ingredient>, Ingredient> IngredientRepository { get; }
        void Save();
    }
}
