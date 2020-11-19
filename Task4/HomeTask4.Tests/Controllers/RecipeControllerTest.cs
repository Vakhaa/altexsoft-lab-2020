using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace HomeTask4.Tests.Controllers
{
    public class RecipeControllerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock; // Create mock object for IUnitOfWork
        private readonly Mock<IRepository> _repositoryMock;  // Create mock object for IRepository
        private readonly RecipeController _controller;
        private readonly Recipe _expectedRecipe;
        private readonly List<Recipe> _expectedListRecipe;
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
            var listRecipes = await _controller.GetRecipesAsync();

            // Assert
            Assert.Same(_expectedListRecipe, listRecipes);
            _repositoryMock.VerifyAll();
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
            _repositoryMock.VerifyAll();
            Assert.NotNull(_controller.CurrentRecipe);
            Assert.Same(_expectedRecipe, _controller.CurrentRecipe);
        }
        [Fact]
        public async Task CreateRecipe_IfNew_CreateRecipe()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository_ReturnNull();
            MakeMockAddForRepository();
            int expectedCategoryId = 1;
            string expectedDescription = "expected";
            string expectedName = "expected";
            
            // Act
            // Run method which should be tested
            await _controller.CreateRecipeAsync(expectedName, expectedCategoryId, expectedDescription);

            // Assert
            Assert.NotNull(_controller.CurrentRecipe);
            Assert.Equal(expectedName, _controller.CurrentRecipe.Name);
            Assert.Equal(expectedCategoryId, _controller.CurrentRecipe.CategoryId);
            Assert.Equal(expectedDescription, _controller.CurrentRecipe.Description);
            _repositoryMock.VerifyAll();
        }
        [Fact]
        public async Task AddIngredientsInRecipe_IfNewItem_AddItem()
        {
            // Arrange
            var ingredientsInRecipes = new List<IngredientsInRecipe>();
            _controller.CurrentRecipe = new Recipe { Id = 1};
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

            // Act
            // Run method which should be tested
            for (int steps = 0; steps < ingredientsId.Count; steps++)
            {
                ingredientsInRecipes
                    .Add(new IngredientsInRecipe(_controller.CurrentRecipe.Id, ingredientsId[steps], countIngredietns[steps]));
            }
            
            await _controller.AddedIngredientsInRecipeAsync(ingredientsId, countIngredietns);

            // Assert
            _repositoryMock.VerifyAll();
            _repositoryMock.Verify(o => o.AddRangeAsync(
                It.Is<List<IngredientsInRecipe>>(entity => entity.Count == ingredientsInRecipes.Count)), Times.Once);
        }
        [Fact]
        public async Task AddStepsInRecipe_IfNewItem_AddItem()
        {
            // Arrange
            _controller.CurrentRecipe = new Recipe { Id = 1};
            var stepsInRecipes = new List<StepsInRecipe>();
            var step = "expected";

            var stepsInRecipe = new List<string>
            {
                step
            };
            
            // Act
            // Run method which should be tested
            for (int it = 0; it< stepsInRecipe.Count; it++)
            {
                stepsInRecipes.Add(new StepsInRecipe(_controller.CurrentRecipe.Id,stepsInRecipe[it]));
            }

            await _controller.AddedStepsInRecipeAsync(stepsInRecipe);
            
            // Assert
            _repositoryMock.Verify(o => o.AddRangeAsync(It.Is<List<StepsInRecipe>>(entity => entity.Count == stepsInRecipes.Count)), Times.Once);
            _repositoryMock.VerifyAll();
        }
        [Fact]
        public async Task FindRecipe_IfExists_ReturnRecipe()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository();
            
            // Act
            // Run method which should be tested
            var recipe = await _controller.FindRecipeAsync(1);

            // Assert
            _repositoryMock.VerifyAll();
            Assert.Equal(_expectedRecipe, recipe);
        }
        [Fact]
        public async Task FindRecipe_IfNotExists_ReturnNull()
        {
            //Arrange
            MakeMockGetWithIncludeEntityForRepository_ReturnNull();
            // Act
            // Run method which should be tested
            var recipe = await _controller.FindRecipeAsync(1);

            // Assert
            Assert.Null(recipe);
            _repositoryMock.VerifyAll();
        }
        private void MakeMockAddForRepository()
        {
            // Simulate "AddAsync" method from "IRepository" to return test entity
            _repositoryMock.Setup(o => o.AddAsync<Recipe>(It.IsAny<Recipe>()))
                .ReturnsAsync((Recipe x) => x);
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
        private void MakeMockGetWithIncludeEntityForRepository_ReturnNull()
        {
            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test list of entities
            _repositoryMock.Setup(o =>
            o.GetWithIncludeEntityAsync<Recipe>(It.IsAny<Func<Recipe, bool>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>()));
        }
    }
}
