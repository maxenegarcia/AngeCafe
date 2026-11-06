using AngeCafe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AngeCafe.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuItemVariant> MenuItemVariants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItemVariant>()
                .HasKey(v => v.VariantId);

            modelBuilder.Entity<MenuItem>()
                .HasMany(m => m.Variants)
                .WithOne(v => v.MenuItem)
                .HasForeignKey(v => v.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItemVariant>()
                .Property(v => v.Price)
                .HasPrecision(10, 2);
        }
    }
}