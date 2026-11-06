namespace AngeCafe.API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        // One category can have many menu items
        public ICollection<MenuItem> MenuItems { get; set; }
            = new List<MenuItem>();
    }
}