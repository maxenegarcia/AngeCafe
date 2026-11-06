namespace AngeCafe.API.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }

        public int CategoryId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        // Relationship with Category
        public Category? Category { get; set; }

        // One menu item can have multiple variants
        public ICollection<MenuItemVariant> Variants { get; set; }
            = new List<MenuItemVariant>();
    }
}