using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await this._cartService.GetCart(id);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            await this._cartService.AddToCart(cart);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var msg = await this._cartService.RemoveFromCart(id);
            return Ok(new
            {
                message = msg
            });
        }

        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var msg = await this._cartService.Checkout(userId);
            return Ok(new
            {
                message = msg
            });
        }

        [HttpGet("bill/{userId}")]

        public async Task<IActionResult> GetBill(int userId)
        {
            var bill = await _cartService.GetBill(userId);
            return Ok(bill);
        }

        [HttpPut("updateQuantity/{cartId}")]
        public async Task<IActionResult> UpdateQuantity(int cartId,[FromBody] int quantity)
        {
            var result = await _cartService.UpdateQuantity(cartId, quantity);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new { message = "Quantity updated successfully" });
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var order = await this._cartService.GetOrders(id);
            return Ok(order);
        }
    }
}
