using System;
using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientsInRecipe> IngredientsInRecipes { get; set; }
        public DbSet<StepsInRecipe> StepsInRecipes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new RecipeConfig());
            modelBuilder.ApplyConfiguration(new StepsInRecipeConfig());
            modelBuilder.ApplyConfiguration(new IngredientsInRecipeConfig());
            modelBuilder.ApplyConfiguration(new IngredientConfig());
        }

    }
}
