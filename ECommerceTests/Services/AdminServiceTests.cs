using ECommerce.Services;

namespace ECommerce.Tests.Services
{
    [TestClass]
    public class AdminServiceTests
    {
        private AdminService _adminService;

        [TestInitialize] 
        public void Init() 
        {
            _adminService = new AdminService();
        }

        [TestMethod]

        public async Task AdLoginSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            string email = "admin@mail";
            string password = "admin";

            // Act
            var result = await _adminService.AdLogin(email, password);

            // Assert
            Assert.AreEqual("Login Success", result);
        }
    }
}
