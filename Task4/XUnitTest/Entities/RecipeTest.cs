using System;
using HomeTask4.Core.Entities;
using Xunit;

namespace XUnitTest.Entities
{
    public class RecipeTest
    {
        [Fact]
        public async void CTOR_Recipe_IfDataCorrect_CreateEntity()
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
