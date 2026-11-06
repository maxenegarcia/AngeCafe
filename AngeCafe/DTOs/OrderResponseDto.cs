namespace AngeCafe.API.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }

        public string QueueNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public string OrderStatus { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public List<OrderItemResponseDto> Items { get; set; }
            = new List<OrderItemResponseDto>();
    }

    public class OrderItemResponseDto
    {
        public string ItemName { get; set; } = string.Empty;

        public string VariantName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }
    }
}
