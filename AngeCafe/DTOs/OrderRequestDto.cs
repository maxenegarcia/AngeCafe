namespace AngeCafe.API.DTOs
{
    public class OrderRequestDto
    {
        public string CustomerName { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public List<OrderItemRequestDto> Items { get; set; }
            = new List<OrderItemRequestDto>();
    }

    public class OrderItemRequestDto
    {
        public int VariantId { get; set; }

        public int Quantity { get; set; }
    }
}
