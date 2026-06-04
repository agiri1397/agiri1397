using HoneywellApp.Domain.Entities;

namespace HoneywellApp.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task UpsertRangeAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetUnsyncedAsync(CancellationToken cancellationToken = default);
    Task MarkAsSyncedAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
}
