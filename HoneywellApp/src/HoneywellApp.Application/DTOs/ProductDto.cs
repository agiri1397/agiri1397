namespace HoneywellApp.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public bool IsSynced { get; set; }
}

public class ProductListResponse
{
    public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
    public bool IsFromCache { get; set; }
    public DateTime? LastSyncedAt { get; set; }
    public string? ErrorMessage { get; set; }
}
