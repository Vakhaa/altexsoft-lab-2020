using System;
using HomeTask4.Core.Entities;
using Xunit;

namespace XUnitTest.Entities
{
    public class StepsInRecipeTest
    {
        [Fact]
        public void CTOR_StepsInRecipe()
        {
            // Arrange
            var rnd = new Random();
            var recipeId = rnd.Next();
            var description = Guid.NewGuid().ToString();

            // Act
            // Run method which should be tested

            var newChildEntity = new StepsInRecipe(recipeId, description);
            
            // Assert
            Assert.Equal(recipeId, newChildEntity.RecipeId);
            Assert.Equal(description, newChildEntity.Description);
        }
    }
}
