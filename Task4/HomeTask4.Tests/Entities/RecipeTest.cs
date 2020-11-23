using HomeTask4.Core.Entities;
using Xunit;

namespace HomeTask4.Tests.Entities
{
    public class RecipeTest
    {
        [Fact]
        public void Constructor_Recipe_IfDataCorrect_CreateEntity()
        {
            // Arrange
            var categoryId = 1;
            var name = "expected";
            var description = "expected";

            // Act
            // Run method which should be tested

            var result = new Recipe(name, categoryId, description);
            // Assert
            Assert.Equal(name, result.Name);
            Assert.Equal(categoryId, result.CategoryId);
            Assert.Equal(description, result.Description);
        }
    }
}
