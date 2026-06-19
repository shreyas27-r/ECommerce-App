using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Services;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetAllProducts();

            var result = products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.ImageUrl,
                p.Price,
                Category = p.Category.ToString()
            });

            return Ok(result);
        }

        [HttpPost("add")]

        public async Task<IActionResult> AddProduct(Product product)
        {
            await _service.AddProduct(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var msg = await _service.DeleteProduct(id);

            return Ok(new
            {
                message = msg
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct([FromBody] Product product,int id)
        {
            var updatedProduct = await _service.EditProduct(product,id);
            if (updatedProduct == null)
            {
                return NotFound(new
                {
                    message = "Product not found"
                });
            }
            return Ok(updatedProduct);
        }


        [HttpGet("users")]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetUsers();
            return Ok(users);
        }

    }
}
