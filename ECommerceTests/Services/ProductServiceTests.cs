using ECommerce.Data;
using ECommerce.Services;
using ECommerce.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Tests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private ProductService _service;
        private AppDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = TestDbContextFactory.Create();
            _service = new ProductService(_context);
        }

        [TestCleanup]   
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]

        public async Task AddProduct_AndGetTheProductCount()
        {
            // Arrange
            var product = new ECommerce.Models.Product
            {
                Name = "Test Product",
                Description = "Test Description",
                ImageUrl = "http://example.com/image.jpg",
                Price = 9.99m,
                Category = (Enum.Category)1
            };
            // Act
            await _service.AddProduct(product);
            var products = await _service.GetAllProducts();
            // Assert
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]

        public async Task DeleteProduct_ShouldTheSuccessMessage()
        {
            // Arrange
            var product = new ECommerce.Models.Product
            {
                Name = "Test Product",
                Description = "Test Description",
                ImageUrl = "http://example.com/image.jpg",
                Price = 9.99m,
                Category = (Enum.Category)1
            };
            // Act
            await _service.AddProduct(product);
            var products = await _service.GetAllProducts();
            Assert.AreEqual(1, products.Count);

            var result = await _service.DeleteProduct(product.Id);
            Assert.AreEqual("Product deleted successfully", result);
        }

        [TestMethod]

        public async Task EditProduct_ShouldReplaceTheOldProductDetails()
        {
            // Arrange
            var product = new ECommerce.Models.Product
            {
                Name = "Test Product",
                Description = "Test Description",
                ImageUrl = "http://example.com/image.jpg",
                Price = 9.99m,
                Category = (Enum.Category)1
            };

            await _service.AddProduct(product);

            var updatedproduct = new ECommerce.Models.Product
            {
                Name = "Updated Product",
                Description = "Updated Description",
                ImageUrl = "http://example.com/image.jpg",
                Price = 9.99m,
                Category = (Enum.Category)1
            };

            var result = await _service.EditProduct( updatedproduct, product.Id);
            Assert.AreEqual("Updated Product", result.Name);
        }
    }
    
}
