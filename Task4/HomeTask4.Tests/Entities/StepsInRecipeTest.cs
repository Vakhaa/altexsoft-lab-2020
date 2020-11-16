using HomeTask4.Core.Entities;
using Xunit;

namespace HomeTask4.Tests.Entities
{
    public class StepsInRecipeTest
    {
        [Fact]
        public void Constructor_StepsInRecipe_IfDataCorrect_CreateEntity()
        {
            // Arrange
            var recipeId = 1;
            var description = "expected";

            // Act
            // Run method which should be tested

            var result = new StepsInRecipe(recipeId, description);
            
            // Assert
            Assert.Equal(recipeId, result.RecipeId);
            Assert.Equal(description, result.Description);
        }
    }
}
