using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Reservation;
using ServicesPlatform.Models.OutputModels.Reservation;
using ServicesPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace ServicesPlatform.Tests
{
    [TestClass]
    public class ReservationServiceTests
    {
        private Mock<IReservationRepository> _mockRepo;
        private ReservationService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IReservationRepository>();
            _service = new ReservationService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllReservations()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, ServiceName = "Service1", Price = 100 },
                new Reservation { Id = 2, ServiceName = "Service2", Price = 200 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(reservations);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].ServiceName.Should().Be("Service1");
        }

        [TestMethod]
        public async Task CreateAsync_ShouldCreateReservation()
        {
            // Arrange
            var input = new CreateReservationInputModel
            {
                ServiceName = "New Service",
                Price = 300,
                ReservationDate = System.DateTime.Now
            };

            var reservation = new Reservation
            {
                Id = 1,
                ServiceName = input.ServiceName,
                Price = input.Price,
                ReservationDate = input.ReservationDate
            };

            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Reservation>())).ReturnsAsync(reservation);

            // Act
            var result = await _service.CreateAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.ServiceName.Should().Be("New Service");
            result.Price.Should().Be(300);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
