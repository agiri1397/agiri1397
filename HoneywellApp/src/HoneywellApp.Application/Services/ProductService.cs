using HoneywellApp.Application.DTOs;
using HoneywellApp.Application.Interfaces;
using HoneywellApp.Domain.Entities;
using HoneywellApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HoneywellApp.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ISyncService _syncService;
    private readonly IApiProductClient? _apiProductClient;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        ISyncService syncService,
        ILogger<ProductService> logger,
        IApiProductClient? apiProductClient = null)
    {
        _productRepository = productRepository;
        _syncService = syncService;
        _logger = logger;
        _apiProductClient = apiProductClient;
    }

    public async Task<ProductListResponse> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        bool isOnline = await _syncService.CheckConnectivityAsync(cancellationToken);

        if (isOnline && _apiProductClient != null)
        {
            try
            {
                var apiProducts = await _apiProductClient.GetAllProductsAsync(cancellationToken);
                if (apiProducts != null)
                {
                    var entities = apiProducts.Select(MapToEntity).ToList();
                    await _productRepository.UpsertRangeAsync(entities, cancellationToken);

                    return new ProductListResponse
                    {
                        Products = entities.Select(MapToDto),
                        IsFromCache = false,
                        LastSyncedAt = DateTime.UtcNow
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch products from API, falling back to local cache");
            }
        }

        var localProducts = await _productRepository.GetAllAsync(cancellationToken);
        return new ProductListResponse
        {
            Products = localProducts.Select(MapToDto),
            IsFromCache = true,
            LastSyncedAt = null
        };
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return product == null ? null : MapToDto(product);
    }

    public async Task<ProductDto?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetBySkuAsync(sku, cancellationToken);
        return product == null ? null : MapToDto(product);
    }

    public async Task<ProductListResponse> GetProductsByCategoryAsync(string category, CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetByCategoryAsync(category, cancellationToken);
        return new ProductListResponse
        {
            Products = products.Select(MapToDto),
            IsFromCache = true
        };
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.IsSynced = false;
        var created = await _productRepository.AddAsync(entity, cancellationToken);

        if (await _syncService.CheckConnectivityAsync(cancellationToken) && _apiProductClient != null)
        {
            try
            {
                var synced = await _apiProductClient.CreateProductAsync(dto, cancellationToken);
                if (synced != null)
                {
                    created.IsSynced = true;
                    await _productRepository.UpdateAsync(created, cancellationToken);
                    return synced;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not sync new product to API");
            }
        }

        return MapToDto(created);
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsSynced = false;
        await _productRepository.UpdateAsync(entity, cancellationToken);

        if (await _syncService.CheckConnectivityAsync(cancellationToken) && _apiProductClient != null)
        {
            try
            {
                await _apiProductClient.UpdateProductAsync(dto, cancellationToken);
                entity.IsSynced = true;
                await _productRepository.UpdateAsync(entity, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not sync updated product to API");
            }
        }

        return MapToDto(entity);
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken = default)
    {
        await _productRepository.DeleteAsync(id, cancellationToken);

        if (await _syncService.CheckConnectivityAsync(cancellationToken) && _apiProductClient != null)
        {
            try { await _apiProductClient.DeleteProductAsync(id, cancellationToken); }
            catch (Exception ex) { _logger.LogWarning(ex, "Could not sync delete to API"); }
        }
    }

    public async Task<bool> SyncProductsAsync(CancellationToken cancellationToken = default)
    {
        if (!await _syncService.CheckConnectivityAsync(cancellationToken) || _apiProductClient == null)
            return false;

        try
        {
            var unsynced = await _productRepository.GetUnsyncedAsync(cancellationToken);
            foreach (var product in unsynced)
            {
                await _apiProductClient.UpdateProductAsync(MapToDto(product), cancellationToken);
            }
            await _productRepository.MarkAsSyncedAsync(unsynced.Select(p => p.Id), cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Product sync failed");
            return false;
        }
    }

    private static ProductDto MapToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        SKU = p.SKU,
        Description = p.Description,
        Price = p.Price,
        StockQuantity = p.StockQuantity,
        Category = p.Category,
        Barcode = p.Barcode,
        UpdatedAt = p.UpdatedAt,
        IsSynced = p.IsSynced
    };

    private static Product MapToEntity(ProductDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        SKU = dto.SKU,
        Description = dto.Description,
        Price = dto.Price,
        StockQuantity = dto.StockQuantity,
        Category = dto.Category,
        Barcode = dto.Barcode,
        UpdatedAt = dto.UpdatedAt == default ? DateTime.UtcNow : dto.UpdatedAt,
        IsSynced = dto.IsSynced
    };
}

public interface IApiProductClient
{
    Task<IEnumerable<ProductDto>?> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> CreateProductAsync(ProductDto product, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(ProductDto product, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken = default);
}
