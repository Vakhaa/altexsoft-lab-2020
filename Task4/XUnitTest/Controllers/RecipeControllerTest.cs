using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace XUnitTest.Controllers
{
    public class RecipeControllerTest
    {
        [Fact]
        public async void GetRecipe()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var name2 = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var description2 = Guid.NewGuid().ToString();
            var categoryId = rnd.Next();
            var categoryId2 = rnd.Next();

            // Simulate "GetWithIncludeListAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o => 
            o.GetWithIncludeListAsync<Recipe>(It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>()))
                .ReturnsAsync(new List<Recipe>
                {
                    new Recipe { Id = 1, Name=name, CategoryId = categoryId, Description = description},
                    new Recipe { Id = 2, Name=name2, CategoryId = categoryId2, Description = description2}
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new RecipeController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            var entities = await controller.GetRecipesAsync();
            var firstEntity = entities.FirstOrDefault();
            var secondEntity = entities.FirstOrDefault(i=>i.Id == 2);

            // Assert
            // Check if the entity has been return from method GetRecipesAsync
            Assert.Equal(name, firstEntity.Name);
            Assert.Equal(1, firstEntity.Id);
            Assert.Equal(categoryId, firstEntity.CategoryId);
            Assert.Equal(description, firstEntity.Description);

            Assert.Equal(name2, secondEntity.Name);
            Assert.Equal(2, secondEntity.Id);
            Assert.Equal(categoryId2, secondEntity.CategoryId);
            Assert.Equal(description2, secondEntity.Description);

            Assert.Equal(2, entities.Count());
        }
        [Fact]
        public async void CreateRecipe_If_Exists()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var categoryId = rnd.Next();

            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test entity
            repositoryMock.Setup(o =>
            o.GetWithIncludeEntityAsync<Recipe>(It.IsAny<Func<Recipe, bool>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>()))
                .ReturnsAsync(new Recipe { Id = 1, Name = name, CategoryId = categoryId, Description = description });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new RecipeController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            await controller.CreateRecipeAsync(name, categoryId ,description);

            // Assert
            // Check if the entity has been return from method CreateRecipeAsync
            Assert.NotNull(controller.CurrentRecipe);
            Assert.Equal(1, controller.CurrentRecipe.Id);
            Assert.Equal(categoryId, controller.CurrentRecipe.CategoryId);
            Assert.Equal(description, controller.CurrentRecipe.Description);
        }
        [Fact]
        public async void CreateRecipe_If_New()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var categoryId = rnd.Next();
            
            // Simulate "AddAsync" method from "IRepository"
            repositoryMock.Setup(o =>
            o.AddAsync<Recipe>(It.IsAny<Recipe>()))
                .ReturnsAsync((Recipe x)=>x);

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new RecipeController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            await controller.CreateRecipeAsync(name, categoryId, description);


            // Assert
            Assert.NotNull(controller.CurrentRecipe);
            Assert.Equal(0, controller.CurrentRecipe.Id);
            Assert.Equal(categoryId, controller.CurrentRecipe.CategoryId);
            Assert.Equal(description, controller.CurrentRecipe.Description);
            repositoryMock.Verify(o => o.AddAsync(It.IsAny<Recipe>()), Times.Exactly(1));
        }
        [Fact]
        public async void AddIngredientsInRecipe()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var count1 = Guid.NewGuid().ToString();
            var count2 = Guid.NewGuid().ToString();
            var ingredientId1 = rnd.Next();
            var ingredientId2 = rnd.Next();

            var ingredientsId = new List<int>
            {
                ingredientId1,
                ingredientId2
            };
            var countIngredietns = new List<string>
            {
                count1,
                count2
            };

            // Simulate "AddRangeAsync" method from "IRepository"
            repositoryMock.Setup(o =>
            o.AddRangeAsync<IngredientsInRecipe>(It.IsAny<List<IngredientsInRecipe>>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new RecipeController(unitOfWorkMock.Object);
            controller.CurrentRecipe = new Recipe { Id = rnd.Next() };
            // Act
            // Run method which should be tested

            await controller.AddedIngredientsInRecipeAsync(ingredientsId,countIngredietns);


            // Assert
            repositoryMock.Verify(o => o.AddRangeAsync(It.IsAny<List<IngredientsInRecipe>>()), Times.Exactly(1));
        }
        [Fact]
        public async void AddStepsInRecipe()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var step1 = Guid.NewGuid().ToString();
            var step2 = Guid.NewGuid().ToString();
            
            var stepsInRecipe = new List<string>
            {
                step1,
                step2
            };

            // Simulate "AddRangeAsync" method from "IRepository"
            repositoryMock.Setup(o =>
            o.AddRangeAsync<StepsInRecipe>(It.IsAny<List<StepsInRecipe>>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new RecipeController(unitOfWorkMock.Object);
            controller.CurrentRecipe = new Recipe { Id = rnd.Next() };
            
            // Act
            // Run method which should be tested
            await controller.AddedStepsInRecipeAsync(stepsInRecipe);

            // Assert
            repositoryMock.Verify(o => o.AddRangeAsync(It.IsAny<List<StepsInRecipe>>()), Times.Exactly(1));
        }
        [Fact]
        public async void FindRecipe()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>(); // Create mock object for IRepository
            var rnd = new Random();

            var id = rnd.Next();
            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var categoryId = rnd.Next();

            // Simulate "GetWithIncludeEntityAsync" method from "IRepository" to return test list of entities
            repositoryMock.Setup(o =>
            o.GetWithIncludeEntityAsync<Recipe>(It.IsAny<Func<Recipe, bool>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>(), It.IsAny<Expression<Func<Recipe, object>>>()))
                .ReturnsAsync(new Recipe 
                { 
                    Id = id, 
                    Name = name, 
                    CategoryId = categoryId, 
                    Description = description 
                });

            var unitOfWorkMock = new Mock<IUnitOfWork>(); // Create mock object for IUnitOfWork

            // Simulate "Repository" property to return prevously created mock object for IRepository
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(o => o.SaveAsync());

            // Create controller which should be tested
            var controller = new RecipeController(unitOfWorkMock.Object);

            // Act
            // Run method which should be tested

            var entity = await controller.FindRecipeAsync(id);

            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(name, entity.Name);
            Assert.Equal(categoryId, entity.CategoryId);
            Assert.Equal(description, entity.Description);
        }
    }
}
