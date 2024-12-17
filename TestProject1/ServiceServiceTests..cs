using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Service;
using ServicesPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace ServicesPlatform.Tests
{
    [TestClass]
    public class ServiceServiceTests
    {
        private Mock<IServiceRepository> _mockRepo;
        private ServiceService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IServiceRepository>();
            _service = new ServiceService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllServices()
        {
            var services = new List<Service>
            {
                new Service { Id = 1, Name = "Service1", Price = 100 },
                new Service { Id = 2, Name = "Service2", Price = 200 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(services);


            var result = await _service.GetAllAsync();

        
            result.Should().HaveCount(2);
            result.Should().Contain(s => s.Name == "Service1");
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCreateService()
        {
          
            var input = new CreateServiceInputModel
            {
                Name = "New Service",
                Price = 300,
                CategoryId = 1
            };

            var service = new Service
            {
                Id = 1,
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId
            };

            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Service>())).ReturnsAsync(service);

      
            var result = await _service.CreateAsync(input);

       
            result.Should().NotBeNull();
            result.Name.Should().Be("New Service");
        }
    }
}
