using AngeCafe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Data
{
    public static class MenuSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Prevent duplicate menu records
            if (await context.Categories.AnyAsync())
            {
                return;
            }

            var categories = new List<Category>
            {
                // 1. ANGE BEVERAGES
                new Category
                {
                    CategoryName = "ANGE BEVERAGES",
                    MenuItems = new List<MenuItem>
                    {
                        CreateItem("Americano",
                            new MenuItemVariant { VariantName = "Hot", Price = 68m },
                            new MenuItemVariant { VariantName = "Cold", Price = 135m }),

                        CreateItem("Latte",
                            new MenuItemVariant { VariantName = "Hot", Price = 135m },
                            new MenuItemVariant { VariantName = "Cold", Price = 160m }),

                        CreateItem("Cappuccino",
                            new MenuItemVariant { VariantName = "Hot", Price = 135m },
                            new MenuItemVariant { VariantName = "Cold", Price = 190m }),

                        CreateItem("Spanish Latte",
                            new MenuItemVariant { VariantName = "Hot", Price = 150m },
                            new MenuItemVariant { VariantName = "Cold", Price = 170m }),

                        CreateItem("Dirty Matcha",
                            new MenuItemVariant { VariantName = "Hot", Price = 160m },
                            new MenuItemVariant { VariantName = "Cold", Price = 180m }),

                        CreateItem("Dark Chocolate",
                            new MenuItemVariant { VariantName = "Hot", Price = 125m },
                            new MenuItemVariant { VariantName = "Cold", Price = 140m }),

                        CreateItem("Matcha Latte",
                            new MenuItemVariant { VariantName = "Hot", Price = 145m },
                            new MenuItemVariant { VariantName = "Cold", Price = 180m }),

                        CreateItem("Flavored Milk Drinks",
                            new MenuItemVariant { VariantName = "Banana", Price = 58m },
                            new MenuItemVariant { VariantName = "Melon", Price = 58m },
                            new MenuItemVariant { VariantName = "Strawberry", Price = 58m }),

                        CreateItem("Yogurt Drinks",
                            new MenuItemVariant { VariantName = "Blueberry", Price = 35m },
                            new MenuItemVariant { VariantName = "Strawberry", Price = 35m }),

                        CreateItem("Mineral Water",
                            new MenuItemVariant { VariantName = "Regular", Price = 20m })
                    }
                },

                // 2. ANGE BREAD & PASTRIES
                new Category
                {
                    CategoryName = "ANGE BREAD & PASTRIES",
                    MenuItems = new List<MenuItem>
                    {
                        CreateItem("Choco Crinkles",
                            new MenuItemVariant { VariantName = "Per Pack", Price = 96m }),

                        CreateItem("Chewy Brownies",
                            new MenuItemVariant { VariantName = "Per Pack", Price = 100m }),

                        CreateItem("Choco Chip Cookies",
                            new MenuItemVariant { VariantName = "Per Pack", Price = 100m }),

                        CreateItem("Cinnamon Butterfly",
                            new MenuItemVariant { VariantName = "Per Piece", Price = 120m }),

                        CreateItem("Kaugian Amann Pastry",
                            new MenuItemVariant { VariantName = "Per Piece", Price = 120m }),

                        CreateItem("Pain au Chocolat",
                            new MenuItemVariant { VariantName = "Per Piece", Price = 130m }),

                        CreateItem("Almond Frangipane",
                            new MenuItemVariant { VariantName = "Per Piece", Price = 140m }),

                        CreateItem("Cromboloni Pastry",
                            new MenuItemVariant { VariantName = "Per Piece", Price = 140m }),

                        CreateItem("Triple Decker Mousse",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 110m }),

                        CreateItem("Red Velvet Cake",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 125m }),

                        CreateItem("Strawberry Creme Cake",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 140m }),

                        CreateItem("Pistachio Chocolate Cake",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 205m }),

                        CreateItem("Blueberry Cheesecake",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 180m }),

                        CreateItem("Oreo Cheesecake",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 180m }),

                        CreateItem("Biscoff Cheesecake",
                            new MenuItemVariant { VariantName = "Per Slice", Price = 260m })
                    }
                },

                // 3. WHITE WONDERS
                new Category
                {
                    CategoryName = "WHITE WONDERS",
                    MenuItems = new List<MenuItem>
                    {
                        CreateItem("Creamy White Chocolate Latte",
                            new MenuItemVariant { VariantName = "Hot (12oz)", Price = 125m },
                            new MenuItemVariant { VariantName = "Iced (16oz)", Price = 140m }),

                        CreateItem("White Chocolate Biscoff Latte",
                            new MenuItemVariant { VariantName = "Hot (12oz)", Price = 135m },
                            new MenuItemVariant { VariantName = "Iced (16oz)", Price = 170m }),

                        CreateItem("White Mocha Latte",
                            new MenuItemVariant { VariantName = "Hot (12oz)", Price = 155m },
                            new MenuItemVariant { VariantName = "Iced (16oz)", Price = 195m }),

                        CreateItem("White Mocha Biscoff Latte",
                            new MenuItemVariant { VariantName = "Hot (12oz)", Price = 160m },
                            new MenuItemVariant { VariantName = "Iced (16oz)", Price = 200m })
                    }
                },

                // 4. BISCOFF LATTE
                new Category
                {
                    CategoryName = "BISCOFF LATTE",
                    MenuItems = new List<MenuItem>
                    {
                        CreateItem("Biscoff Espresso Latte",
                            new MenuItemVariant { VariantName = "Hot", Price = 175m },
                            new MenuItemVariant { VariantName = "Cold", Price = 210m }),

                        CreateItem("Biscoff Latte (Non-Coffee)",
                            new MenuItemVariant { VariantName = "Hot", Price = 155m },
                            new MenuItemVariant { VariantName = "Cold", Price = 190m })
                    }
                }
            };

            context.Categories.AddRange(categories);

            await context.SaveChangesAsync();
        }

        private static MenuItem CreateItem(
            string name,
            params MenuItemVariant[] variants)
        {
            return new MenuItem
            {
                ItemName = name,
                IsActive = true,
                Variants = variants.ToList()
            };
        }
    }
}
