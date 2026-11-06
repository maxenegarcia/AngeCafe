namespace AngeCafe.API.DTOs
{
    public class MenuItemResponseDto
    {
        public int MenuItemId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public List<MenuItemVariantResponseDto> Variants { get; set; } = new();
    }

    public class MenuItemVariantResponseDto
    {
        public int VariantId { get; set; }
        public string VariantName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}