namespace AngeCafe.API.Models
{
    public class MenuItemVariant
    {
        public int VariantId { get; set; }

        public int MenuItemId { get; set; }

        public string VariantName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public MenuItem? MenuItem { get; set; }
    }
}