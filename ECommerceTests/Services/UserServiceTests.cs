using ECommerce.Data;
using ECommerce.Services;
using ECommerce.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private AppDbContext _context;

        [TestInitialize]
        public void setup()
        {
            _context = TestDbContextFactory.Create();
            _userService = new UserService(_context);
        }

        [TestCleanup]
        public void cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]

        public async Task CreateUser_ShouldCreateNewUser()
        {
            // Arrange
            var user = new ECommerce.Models.User
            {
                Name = "Test User",
                Email = "user@mail",
                Password = "password"
            };

            var result = await _userService.CreateUser(user);
            Assert.AreEqual("User Registered", result);
        }

        [TestMethod]

        public async Task UserLogin_ShouldSuccessForValidCredentials()
        {
            var user = new ECommerce.Models.User
            {
                Name = "Test User",
                Email = "user@mail",
                Password = "password"
            };

             await _userService.CreateUser(user);
             var result = await _userService.Login(user.Email, user.Password); 

            Assert.AreEqual(result, user);
        }
    } 
}

