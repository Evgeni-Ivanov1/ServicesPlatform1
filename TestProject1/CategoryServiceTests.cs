using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Category;
using ServicesPlatform.Models.OutputModels.Category;
using ServicesPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace ServicesPlatform.Tests
{
    [TestClass]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _mockCategoryRepository;
        private CategoryService _categoryService;

        [TestInitialize]
        public void Setup()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllCategories()
        {
    
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1", Description = "Description1" },
                new Category { Id = 2, Name = "Category2", Description = "Description2" }
            };

            _mockCategoryRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(categories);


            var result = await _categoryService.GetAllAsync();

  
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Category1");
            result[1].Description.Should().Be("Description2");
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnCreatedCategory()
        {
            // Arrange
            var inputModel = new CreateCategoryInputModel
            {
                Name = "New Category",
                Description = "New Description"
            };

            var createdCategory = new Category
            {
                Id = 1,
                Name = inputModel.Name,
                Description = inputModel.Description
            };

            _mockCategoryRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Category>()))
                .ReturnsAsync(createdCategory);

            // Act
            var result = await _categoryService.CreateAsync(inputModel);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("New Category");
            result.Description.Should().Be("New Description");
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldUpdateCategory()
        {

            var inputModel = new UpdateCategoryInputModel
            {
                Id = 1,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            var existingCategory = new Category
            {
                Id = 1,
                Name = "Old Name",
                Description = "Old Description"
            };

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingCategory);
            _mockCategoryRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Category>())).ReturnsAsync(existingCategory);


            var result = await _categoryService.UpdateAsync(inputModel);


            result.Should().NotBeNull();
            result.Name.Should().Be("Updated Name");
            result.Description.Should().Be("Updated Description");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdateAsync_ShouldThrowException_WhenCategoryNotFound()
        {
            // Arrange
            var inputModel = new UpdateCategoryInputModel
            {
                Id = 1,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Category)null);

            // Act
            await _categoryService.UpdateAsync(inputModel);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepository_WhenCategoryExists()
        {

            var category = new Category { Id = 1, Name = "Category1" };
            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(category);
            _mockCategoryRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);


            await _categoryService.DeleteAsync(1);


            _mockCategoryRepository.Verify(repo => repo.DeleteAsync(category), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task DeleteAsync_ShouldThrowException_WhenCategoryNotFound()
        {

            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Category)null);

    
            await _categoryService.DeleteAsync(1);

           
        }
    }
}
