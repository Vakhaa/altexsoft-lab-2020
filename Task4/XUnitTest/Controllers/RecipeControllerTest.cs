using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace XUnitTest.Controllers
{
    public class RecipeControllerTest
    {
        Mock<IUnitOfWork> _unitOfWorkMock; // Create mock object for IUnitOfWork
        Mock<IRepository> _repositoryMock;  // Create mock object for IRepository
        RecipeController _controller;
        Recipe _expectedRecipe;
        List<Recipe> _expectedListRecipe;
        public RecipeControllerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IRepository>();

            _expectedRecipe = new Recipe
            {
                Id = 1,
                Name = "expected",
                Description = "expected"
            };

            _expectedListRecipe = new List<Recipe>()
            {
                _expectedRecipe
            };
            // Simulate "AddAsync" method from "IRepository" to return test entity
            _repositoryMock.Setup(o => o.AddAsync<Recipe>(It.IsAny<Recipe>()))
                .ReturnsAsync((Recipe x) => x);

            // Simulate "Repository" property to return prevously created mock object for IRepository
            _unitOfWorkMock.SetupGet(o => o.Repository)
                .Returns(_repositoryMock.Object);

            _unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            _controller = new RecipeController(_unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetRecipe_IfIsItems_ReturnItems()
        {
            // Arrange
            MakeMockGetWithIncludeListForRepository();
            // Act
            // Run method which should be tested
            var result = await _controller.GetRecipesAsync();

            // Assert
            Assert.Same(_expectedListRecipe, result);
        }
        [Fact]
        public async Task CreateRecipe_IfExists_ReturnRecipe()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository();

            // Act
            // Run method which should be tested
            await _controller.CreateRecipeAsync("expected", 1,"expected");

            // Assert
            // Check if the entity has been return from method CreateRecipeAsync
            Assert.NotNull(_controller.CurrentRecipe);
            Assert.Same(_expectedRecipe, _controller.CurrentRecipe);
        }
        [Fact]
        public async Task CreateRecipe_IfNew_CreateRecipe()
        {
            // Act
            // Run method which should be tested
            await _controller.CreateRecipeAsync("expected", 1, "expected");

            // Assert
            Assert.NotNull(_controller.CurrentRecipe);
            Assert.Equal(0, _controller.CurrentRecipe.Id);
            Assert.Equal(1, _controller.CurrentRecipe.CategoryId);
            Assert.Equal("expected", _controller.CurrentRecipe.Description);
            _repositoryMock.Verify(o => o.AddAsync(It.IsAny<Recipe>()), Times.Exactly(1));
        }
        [Fact]
        public async Task AddIngredientsInRecipe_IfNewItem_AddItem()
        {
            // Arrange
            var count = "expected";
            var ingredientId = 1;

            var ingredientsId = new List<int>
            {
                ingredientId
            };
            var countIngredietns = new List<string>
            {
                count
            };
            MakeMockGetWithIncludeListForRepository();
            
            // Act
            // Run method which should be tested
            _controller.CurrentRecipe = new Recipe { Id = 1};
            await _controller.AddedIngredientsInRecipeAsync(ingredientsId,countIngredietns);

            // Assert
            _repositoryMock.Verify(o => o.AddRangeAsync(It.IsAny<List<IngredientsInRecipe>>()), Times.Exactly(1));
        }
        [Fact]
        public async Task AddStepsInRecipe_IfNewItem_AddItem()
        {
            // Arrange
            var step = "expected";
            MakeMockGetWithIncludeEntityForRepository();

            var stepsInRecipe = new List<string>
            {
                step
            };

            // Act
            // Run method which should be tested
            _controller.CurrentRecipe = new Recipe { Id = 1};
            await _controller.AddedStepsInRecipeAsync(stepsInRecipe);

            // Assert
            _repositoryMock.Verify(o => o.AddRangeAsync(It.IsAny<List<StepsInRecipe>>()), Times.Exactly(1));
        }
        [Fact]
        public async Task FindRecipe_IfExists_ReturnRecipe()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository();
            
            // Act
            // Run method which should be tested
            var result = await _controller.FindRecipeAsync(1);

            // Assert
            Assert.Equal(_expectedRecipe, result);
        }
        [Fact]
        public async Task FindRecipe_IfNotExists_ReturnNull()
        {
            // Act
            // Run method which should be tested
            var result = await _controller.FindRecipeAsync(1);

            // Assert
            Assert.Null(result);
        }
        private void MakeMockGetWithIncludeListForRepository()
        {
            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            _repositoryMock.Setup(o =>
            o.GetWithIncludeListAsync<Recipe>(It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>()))
                .ReturnsAsync(_expectedListRecipe);
        }

        private void MakeMockGetWithIncludeEntityForRepository()
        {
            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test list of entities
            _repositoryMock.Setup(o =>
            o.GetWithIncludeEntityAsync<Recipe>(It.IsAny<Func<Recipe, bool>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>()))
                .ReturnsAsync(_expectedRecipe);
        }
    }
}
