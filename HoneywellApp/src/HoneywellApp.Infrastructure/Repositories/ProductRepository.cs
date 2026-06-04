using HoneywellApp.Domain.Entities;
using HoneywellApp.Domain.Interfaces;
using HoneywellApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HoneywellApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default) =>
        await _db.Products.Where(p => !p.IsDeleted).OrderBy(p => p.Name).ToListAsync(ct);

    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _db.Products.FindAsync([id], ct);

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken ct = default) =>
        await _db.Products.FirstOrDefaultAsync(p => p.SKU == sku, ct);

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, CancellationToken ct = default) =>
        await _db.Products.Where(p => p.Category == category && !p.IsDeleted).ToListAsync(ct);

    public async Task<Product> AddAsync(Product product, CancellationToken ct = default)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync(ct);
        return product;
    }

    public async Task UpdateAsync(Product product, CancellationToken ct = default)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var product = await GetByIdAsync(id, ct);
        if (product == null) return;
        product.IsDeleted = true;
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpsertRangeAsync(IEnumerable<Product> products, CancellationToken ct = default)
    {
        foreach (var product in products)
        {
            var existing = await _db.Products.FindAsync([product.Id], ct);
            if (existing == null)
            {
                _db.Products.Add(product);
            }
            else
            {
                existing.Name = product.Name;
                existing.SKU = product.SKU;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.StockQuantity = product.StockQuantity;
                existing.Category = product.Category;
                existing.Barcode = product.Barcode;
                existing.UpdatedAt = product.UpdatedAt;
                existing.IsSynced = true;
            }
        }
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Product>> GetUnsyncedAsync(CancellationToken ct = default) =>
        await _db.Products.Where(p => !p.IsSynced && !p.IsDeleted).ToListAsync(ct);

    public async Task MarkAsSyncedAsync(IEnumerable<int> ids, CancellationToken ct = default)
    {
        var products = await _db.Products.Where(p => ids.Contains(p.Id)).ToListAsync(ct);
        foreach (var p in products) p.IsSynced = true;
        await _db.SaveChangesAsync(ct);
    }
}
