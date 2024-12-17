using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;

namespace ServicesPlatform.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);

            _userService = new UserService(_mockUserManager.Object, _mockRoleManager.Object);
        }

        [TestMethod]
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
        
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1" },
                new ApplicationUser { Id = "2", UserName = "user2" }
            };

            _mockUserManager.Setup(um => um.Users).Returns(users.AsQueryable());

           
            var result = await _userService.GetUsersAsync();

          
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Any(u => u.UserName == "user1").Should().BeTrue();
        }

        [TestMethod]
        public async Task CreateRoleAsync_ShouldCreateRole()
        {
            
            var roleName = "Admin";
            _mockRoleManager
                .Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

          
            await _userService.CreateRoleAsync(roleName);

          
            _mockRoleManager.Verify(rm => rm.CreateAsync(It.Is<IdentityRole>(r => r.Name == roleName)), Times.Once);
        }

        [TestMethod]
        public async Task AssignRoleAsync_ShouldAssignRoleToUser()
        {
            
            var user = new ApplicationUser { Id = "1", UserName = "user1" };
            var roleName = "Admin";

            _mockUserManager.Setup(um => um.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

           
            await _userService.AssignRoleAsync(user.Id, roleName);

    
            _mockUserManager.Verify(um => um.AddToRoleAsync(user, roleName), Times.Once);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
           
            var userId = "1";
            var user = new ApplicationUser { Id = userId, UserName = "user1" };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

          
            var result = await _userService.GetUserByIdAsync(userId);

       
            result.Should().NotBeNull();
            result.UserName.Should().Be("user1");
        }

        [TestMethod]
        public async Task GetRolesAsync_ShouldReturnAllRoles()
        {
 
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
            };

            _mockRoleManager.Setup(rm => rm.Roles).Returns(roles.AsQueryable());

    
            var result = await _userService.GetRolesAsync();

 
            result.Should().HaveCount(2);
            result.Any(r => r.Name == "Admin").Should().BeTrue();
        }
    }
}
