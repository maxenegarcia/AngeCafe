namespace AngeCafe.API.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }

        public int CategoryId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsActive { get; set; } = true;

        // Each menu item belongs to one category
        public Category? Category { get; set; }
    }
}