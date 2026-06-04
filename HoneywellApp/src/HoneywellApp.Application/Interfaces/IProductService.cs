using HoneywellApp.Application.DTOs;

namespace HoneywellApp.Application.Interfaces;

public interface IProductService
{
    Task<ProductListResponse> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProductDto?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<ProductListResponse> GetProductsByCategoryAsync(string category, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken cancellationToken = default);
    Task<ProductDto> UpdateProductAsync(ProductDto product, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> SyncProductsAsync(CancellationToken cancellationToken = default);
}
