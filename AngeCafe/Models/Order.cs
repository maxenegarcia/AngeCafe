namespace AngeCafe.API.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string QueueNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = "Unpaid";

        public string OrderStatus { get; set; } = "Pending";

        public decimal TotalAmount { get; set; }

        // One order can contain multiple items
        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();
    }
}
