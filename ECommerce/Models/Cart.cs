using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }

        // Foreign Key to Product
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        // Navigation Properties
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
