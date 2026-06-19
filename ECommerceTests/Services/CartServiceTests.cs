using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Services;
using ECommerce.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Tests.Services
{
    [TestClass]
    public class CartServiceTests
    {
        private CartService _cartService;
        private AppDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = TestDbContextFactory.Create();
            _cartService = new CartService(_context);
        }
        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddToCart_ShouldAddNewItem()
        {
            // Arrange
            var cart = new ECommerce.Models.Cart
            {
                UserId = 1,
                ProductId = 1
            };
            // Act
            var result = await _cartService.AddToCart(cart);
            // Assert
            Assert.AreEqual("Item added to cart successfully", result);
            var addedItem = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == 1 && c.ProductId == 1);
            Assert.IsNotNull(addedItem);
            Assert.AreEqual(1, addedItem.Quantity);
        }

        [TestMethod]

        public async Task RemoveFromCart_ShouldRemoveTheItemFromCart()
        {
            // Arrange
            var cart = new ECommerce.Models.Cart
            {
                UserId = 1,
                ProductId = 1
            };
            await _cartService.AddToCart(cart);
            var addedItem = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == 1 && c.ProductId == 1);
            // Act
            var result = await _cartService.RemoveFromCart(addedItem.Id);
            // Assert
            Assert.AreEqual("Item removed from cart successfully", result);
            var removedItem = await _context.Carts.FindAsync(addedItem.Id);
            Assert.IsNull(removedItem);
        }

        [TestMethod]

        public async Task GetCart_ShouldReturnAlltheCart()
        {
            _context.Products.Add(new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000
            });

            await _context.SaveChangesAsync();

            var cart = new ECommerce.Models.Cart
            {
                UserId = 1,
                ProductId = 1
            };
            // Act
            await _cartService.AddToCart(cart);

            // Assert

            var totalCarts = await _context.Carts.CountAsync();
            Assert.AreEqual(1, totalCarts);

            var directQuery = await _context.Carts
                .Where(c => c.UserId == 1)
                .ToListAsync();

            Assert.AreEqual(1, directQuery.Count);

            var cartItems = await _cartService.GetCart(1);

            Assert.AreEqual(1, cartItems.Count);
        }

        [TestMethod]

        public async Task Checkout_ShouldRemoveAllItemsFromCart()
        {
            _context.Products.Add(new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000
            });

            await _context.SaveChangesAsync();
            // Arrange
            var cart = new ECommerce.Models.Cart
            {
                UserId = 1,
                ProductId = 1
            };
            await _cartService.AddToCart(cart);
            // Act
            var result = await _cartService.Checkout(1);
            // Assert
            Assert.AreEqual("Checkout successful", result);
            var remainingItems = await _context.Carts.Where(c => c.UserId == 1).ToListAsync();
            Assert.AreEqual(0, remainingItems.Count);
        }

        [TestMethod]

        public async Task Bill_ShouldBeCalculatedCorrectly()
        {
            _context.Products.Add(new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000
            });
            await _context.SaveChangesAsync();
            // Arrange
            var cart = new ECommerce.Models.Cart
            {
                UserId = 1,
                ProductId = 1,
            };
            await _cartService.AddToCart(cart);
            await _cartService.AddToCart(cart);

            // Act
            var bill = await _cartService.GetBill(1);

            var grandTotal = bill.GetType().GetProperty("GrandTotal")?.GetValue(bill);

            Assert.AreEqual(100000m, grandTotal);
        }

        [TestMethod]

        public async Task UpdateQuantity_ShouldUpdateTheQuantityOfCartItem()
        {
            _context.Products.Add(new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000
            });
            await _context.SaveChangesAsync();
            // Arrange
            var cart = new ECommerce.Models.Cart
            {
                UserId = 1,
                ProductId = 1,
            };
            await _cartService.AddToCart(cart);
            // Act
            var result = await _cartService.UpdateQuantity(cart.Id, 5);
            // Assert
            Assert.IsTrue(result);
            var updatedItem = await _context.Carts.FindAsync(cart.Id);
            Assert.AreEqual(5, updatedItem.Quantity);
        }
    }
}
