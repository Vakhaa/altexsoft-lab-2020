using System;
using HomeTask4.Core.Entities;
using Xunit;

namespace XUnitTest.Entities
{
    public class IngredientsInRecipeTest
    {
        [Fact]
        public void CTOR_IngredientInRecipe()
        {
            // Arrange
            var rnd = new Random();
            var recipeId = rnd.Next();
            var ingredientId = rnd.Next();
            var countIngredient = Guid.NewGuid().ToString();

            // Act
            // Run method which should be tested

            var newEntity = new IngredientsInRecipe(recipeId, ingredientId, countIngredient);
            // Assert
            Assert.Equal(recipeId, newEntity.RecipeId);
            Assert.Equal(ingredientId, newEntity.IngredientId);
            Assert.Equal(countIngredient, newEntity.CountIngredient);
        }
    }
}
