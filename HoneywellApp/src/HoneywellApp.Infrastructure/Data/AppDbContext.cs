using HoneywellApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoneywellApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(256).IsRequired();
            e.Property(u => u.PasswordHash).IsRequired();
            e.Property(u => u.JwtToken).HasMaxLength(2048);
            e.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.HasKey(p => p.Id);
            e.HasIndex(p => p.SKU).IsUnique();
            e.Property(p => p.Name).HasMaxLength(256).IsRequired();
            e.Property(p => p.SKU).HasMaxLength(100).IsRequired();
            e.Property(p => p.Description).HasMaxLength(1024);
            e.Property(p => p.Price).HasColumnType("decimal(18,2)");
            e.Property(p => p.Category).HasMaxLength(128);
            e.Property(p => p.Barcode).HasMaxLength(128);
            e.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Seed sample data
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Honeywell PD43 Labels (4x2)",
                SKU = "HW-PD43-LBL-4X2",
                Description = "Thermal direct labels 4-inch x 2-inch, 1000/roll",
                Price = 9.99m,
                StockQuantity = 500,
                Category = "Printer Supplies",
                Barcode = "012345678901",
                IsSynced = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 2,
                Name = "Honeywell PC42t Ribbon 110mm",
                SKU = "HW-PC42T-RBN-110",
                Description = "Thermal transfer ribbon 110mm x 74m, wax/resin",
                Price = 14.50m,
                StockQuantity = 200,
                Category = "Printer Supplies",
                Barcode = "012345678902",
                IsSynced = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 3,
                Name = "Honeywell Voyager 1202g Scanner",
                SKU = "HW-VOY-1202G",
                Description = "Wireless Bluetooth barcode scanner, USB base",
                Price = 249.00m,
                StockQuantity = 15,
                Category = "Scanners",
                Barcode = "012345678903",
                IsSynced = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 4,
                Name = "Shipping Label 6x4 Fanfold",
                SKU = "SHIP-LBL-6X4-FF",
                Description = "Direct thermal shipping labels, 6x4 inch fanfold 500 labels",
                Price = 22.95m,
                StockQuantity = 1000,
                Category = "Shipping Supplies",
                Barcode = "012345678904",
                IsSynced = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 5,
                Name = "Honeywell CT30P Mobile Computer",
                SKU = "HW-CT30P-MC",
                Description = "Android enterprise mobile computer, 5-inch display, 2D imager",
                Price = 899.00m,
                StockQuantity = 8,
                Category = "Mobile Computers",
                Barcode = "012345678905",
                IsSynced = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }

    public static string GetDbPath()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(folder, "HoneywellApp", "app.db");
    }
}
