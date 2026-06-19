using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddToCart(Cart cart)
        {
            var existingItem = await _context.Carts
                .FirstOrDefaultAsync(c =>
                    c.UserId == cart.UserId &&
                    c.ProductId == cart.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Quantity = 1;
                _context.Carts.Add(cart);
            }

            await _context.SaveChangesAsync();

            return "Item added to cart successfully";
        }

        public async Task<List<Cart>> GetCart(int userId)
        {
            return await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<string> RemoveFromCart(int cartId)
        {
            var item = await _context.Carts.FindAsync(cartId);

            if (item == null)
            {
                return "Cart item not found";
            }

            _context.Carts.Remove(item);
            await _context.SaveChangesAsync();

            return "Item removed from cart successfully";
        }

        public async Task<string> Checkout(int userId) 
        {
            var cartItems = await _context.Carts
    .Where(c => c.UserId == userId)
    .Include(c => c.Product)
    .ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                return "Cart is empty";
            }

            foreach (var item in cartItems)
            {
                _context.Orders.Add(new Order
                {
                    UserId = item.UserId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price * item.Quantity,
                    PurchasedOn = DateTime.UtcNow
                });
            }

            _context.Carts.RemoveRange(cartItems);

            await _context.SaveChangesAsync();
            return "Checkout successful";
        }

        public async Task<object> GetBill(int userId)
        {
            decimal totalPrice = 0;
            decimal deliveryFee = 50;

            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();
            if (cartItems.Count < 1)
            {
                return "Cart is empty";
            }
                foreach (var item in cartItems)
                {
                    totalPrice += item.Product.Price * item.Quantity;
                }

                if (totalPrice >= 1000)
                {
                    deliveryFee = 0;
                }

                return new
                {
                    Items = cartItems.Select(c => new
                    {
                        ProductName = c.Product.Name,
                        Price = c.Product.Price,
                        Quantity = c.Quantity,
                        Total = c.Product.Price * c.Quantity
                    }),
                    DeliveryFee = deliveryFee,
                    GrandTotal = totalPrice + deliveryFee
                };
            
        }

        public async Task<bool> UpdateQuantity(int cartId,int quantity)
        {
            var item = await _context.Carts.FindAsync(cartId);
            if(item == null)
            {
                return false;
            }

            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetOrders(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

    }
}
