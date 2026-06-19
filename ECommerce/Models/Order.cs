namespace ECommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchasedOn { get; set; } = DateTime.UtcNow;
    }
}
