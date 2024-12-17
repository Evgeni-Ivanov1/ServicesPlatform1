using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Review;
using ServicesPlatform.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace ServicesPlatform.Tests
{
    [TestClass]
    public class ReviewServiceTests
    {
        private Mock<IReviewRepository> _mockRepo;
        private ReviewService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IReviewRepository>();
            _service = new ReviewService(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetReviewsByServiceIdAsync_ShouldReturnReviews()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Comment = "Great Service", Rating = 5 },
                new Review { Id = 2, Comment = "Average", Rating = 3 }
            };

            _mockRepo.Setup(repo => repo.GetByServiceIdAsync(1)).ReturnsAsync(reviews);

            // Act
            var result = await _service.GetReviewsByServiceIdAsync(1);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(r => r.Comment == "Great Service");
        }

        [TestMethod]
        public async Task AddReviewAsync_ShouldThrowException_WhenCommentIsEmpty()
        {
            // Arrange
            var input = new AddReviewInputModel
            {
                ServiceId = 1,
                UserName = "Test User",
                Comment = "",
                Rating = 4
            };

            // Act
            Func<Task> act = async () => await _service.AddReviewAsync(input, "UserId123");

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                     .WithMessage("Comment cannot be empty.");
        }

        [TestMethod]
        public async Task UpdateReviewAsync_ShouldUpdateReview()
        {
            // Arrange
            var review = new Review { Id = 1, Comment = "Old Comment", Rating = 3 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(review);

            // Act
            await _service.UpdateReviewAsync(1, "New Comment", 5);

            // Assert
            review.Comment.Should().Be("New Comment");
            review.Rating.Should().Be(5);
            _mockRepo.Verify(repo => repo.UpdateReviewAsync(review), Times.Once);
        }
    }
}
