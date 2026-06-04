using HoneywellApp.Application.DTOs;
using HoneywellApp.Application.Services;
using System.Net.Http.Json;

namespace HoneywellApp.Infrastructure.Services;

public class ApiAuthClient : IApiAuthClient
{
    private readonly HttpClient _httpClient;

    public ApiAuthClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<LoginResponse?> AuthenticateAsync(string email, string password, CancellationToken ct = default)
    {
        var payload = new { email, password };
        var response = await _httpClient.PostAsJsonAsync("auth/login", payload, ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(ct);
            return LoginResponse.Failed(error);
        }

        return await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: ct);
    }
}
