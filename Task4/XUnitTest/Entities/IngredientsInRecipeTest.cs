using System;
using HomeTask4.Core.Entities;
using Xunit;

namespace XUnitTest.Entities
{
    public class IngredientsInRecipeTest
    {
        [Fact]
        public void CTOR_IngredientInRecipe_IfDataCorrect_CreateEntity()
        {
            // Arrange
            var recipeId = 1;
            var ingredientId = 1;
            var countIngredient = "expected";

            // Act
            // Run method which should be tested

            var result = new IngredientsInRecipe(recipeId, ingredientId, countIngredient);
            // Assert
            Assert.Equal(recipeId, result.RecipeId);
            Assert.Equal(ingredientId, result.IngredientId);
            Assert.Equal(countIngredient, result.CountIngredient);
        }
    }
}
