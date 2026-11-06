namespace AngeCafe.API.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int VariantId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string VariantName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }

        // Relationship with Order
        public Order? Order { get; set; }

        // Relationship with MenuItemVariant
        public MenuItemVariant? MenuItemVariant { get; set; }
    }
}
