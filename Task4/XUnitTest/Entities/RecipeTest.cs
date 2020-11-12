using System;
using HomeTask4.Core.Entities;
using Xunit;

namespace XUnitTest.Entities
{
    public class RecipeTest
    {
        [Fact]
        public async void CTOR_Recipe()
        {
            // Arrange
            var rnd = new Random();

            var categoryId = rnd.Next();
            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();

            // Act
            // Run method which should be tested

            var newEntity = new Recipe(name, categoryId, description);
            // Assert
            Assert.Equal(name, newEntity.Name);
            Assert.Equal(categoryId, newEntity.CategoryId);
            Assert.Equal(description, newEntity.Description);
        }
    }
}
