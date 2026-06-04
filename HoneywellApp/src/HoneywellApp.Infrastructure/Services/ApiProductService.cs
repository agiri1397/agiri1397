using HoneywellApp.Application.DTOs;
using HoneywellApp.Application.Services;
using System.Net.Http.Json;

namespace HoneywellApp.Infrastructure.Services;

public class ApiProductClient : IApiProductClient
{
    private readonly HttpClient _httpClient;

    public ApiProductClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IEnumerable<ProductDto>?> GetAllProductsAsync(CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync("products", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>(cancellationToken: ct);
    }

    public async Task<ProductDto?> CreateProductAsync(ProductDto product, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsJsonAsync("products", product, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: ct);
    }

    public async Task UpdateProductAsync(ProductDto product, CancellationToken ct = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"products/{product.Id}", product, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(int id, CancellationToken ct = default)
    {
        var response = await _httpClient.DeleteAsync($"products/{id}", ct);
        response.EnsureSuccessStatusCode();
    }
}
