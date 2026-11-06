using System.ComponentModel.DataAnnotations;

namespace AngeCafe.API.DTOs
{
    public class OrderRequestDto
    {
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<OrderItemRequestDto> Items { get; set; }
            = new List<OrderItemRequestDto>();
    }

    public class OrderItemRequestDto
    {
        [Required]
        public int VariantId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
