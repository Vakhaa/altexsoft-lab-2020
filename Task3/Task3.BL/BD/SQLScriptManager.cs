using System;
using System.Collections.Generic;
using System.Linq;
using Task2.BL.DAL;

namespace Task3.BL.BD
{
    public static class SQLScriptManager
    {
        public static bool IsExists<T>(string nameTabel) where T : class
        {
            using (var db = new BookRecipesContext())
            {
                try
                {
                    return db.Database.SqlQuery<T>($"SELECT * FROM {nameTabel}").Any();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public static List<T> SQLQuerry<T>(string querry) where T: class
        {
            using (var db = new BookRecipesContext())
            {
                try
                {
                    return db.Database.SqlQuery<T>(querry).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public static void SQLQuerry(string querry)
        {
            using (var db = new BookRecipesContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand(querry);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        public static void CreateTabelIngredirntsForRecipes(int recipeId)
        {
            using (var db = new BookRecipesContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand($"CREATE TABLE IngredientsInRecipe_{recipeId.ToString()} " +
                         $"(Id INT IDENTITY PRIMARY KEY," +
                          $"IngredientsId INT NOT NULL REFERENCES Ingredients(Id)," +
                           $"CountIngredients NVARCHAR(30) DEFAULT \'1\');");
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        public static void CreateTabelStepsForRecipes(int recipeId)
        {
            using (var db = new BookRecipesContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlCommand($"CREATE TABLE StepsInRecipe_{recipeId.ToString()} " +
                         $"(Id INT IDENTITY PRIMARY KEY," +
                           $"Description NVARCHAR(500) DEFAULT \'Description is empty.\');");
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
