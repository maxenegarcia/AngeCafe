namespace AngeCafe.API.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string QueueNumber { get; set; } = string.Empty;


        public string CustomerName { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();
    }
}