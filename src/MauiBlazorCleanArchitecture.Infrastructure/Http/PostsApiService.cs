using System.Net.Http.Json;
using MauiBlazorCleanArchitecture.Application.Common;
using MauiBlazorCleanArchitecture.Application.DTOs;
using MauiBlazorCleanArchitecture.Application.Interfaces;

namespace MauiBlazorCleanArchitecture.Infrastructure.Http;

/// <summary>
/// Example of consuming a remote REST web service with a typed
/// HttpClient (registered via IHttpClientFactory in DependencyInjection).
/// Uses the free https://jsonplaceholder.typicode.com sandbox API as a
/// stand-in for a real backend - swap BaseAddress for your own API.
/// </summary>
public class PostsApiService : IPostsApiService
{
    private readonly HttpClient _httpClient;

    public PostsApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<List<PostDto>>> GetPostsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var posts = await _httpClient.GetFromJsonAsync<List<PostDto>>("posts", cancellationToken);
            return Result<List<PostDto>>.Success(posts ?? new List<PostDto>());
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or NotSupportedException)
        {
            return Result<List<PostDto>>.Failure($"No se pudo obtener la información del servicio: {ex.Message}");
        }
    }

    public async Task<Result<PostDto>> CreatePostAsync(PostDto post, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("posts", post, cancellationToken);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<PostDto>(cancellationToken: cancellationToken);
            return created is null
                ? Result<PostDto>.Failure("El servicio no devolvió una respuesta válida.")
                : Result<PostDto>.Success(created);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or NotSupportedException)
        {
            return Result<PostDto>.Failure($"No se pudo enviar la información al servicio: {ex.Message}");
        }
    }
}
