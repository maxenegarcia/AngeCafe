namespace AngeCafe.API.DTOs
{
    public class MenuItemRequestDto
    {
        public int CategoryId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;

        public List<MenuItemVariantRequestDto> Variants { get; set; } = new();
    }

    public class MenuItemVariantRequestDto
    {
        public string VariantName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}