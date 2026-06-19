using ECommerce.Enum;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; } = 0;

        public Category Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
