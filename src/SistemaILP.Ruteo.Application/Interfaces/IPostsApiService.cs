using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;

namespace SistemaILP.Ruteo.Application.Interfaces;

/// <summary>
/// Example contract for consuming a remote web service (WS/REST API).
/// Implemented in Infrastructure using a typed HttpClient.
/// </summary>
public interface IPostsApiService
{
    Task<Result<List<PostDto>>> GetPostsAsync(CancellationToken cancellationToken = default);

    Task<Result<PostDto>> CreatePostAsync(PostDto post, CancellationToken cancellationToken = default);
}
