using System.Security.Claims;
using SistemaILP.Ruteo.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace SistemaILP.Ruteo.Infrastructure.Auth;

/// <summary>
/// Blazor AuthenticationStateProvider respaldado por la fila de sesion
/// activa en la tabla "usuario" (a lo sumo una: login hace upsert,
/// logout elimina la fila - ver AuthService). Registrado tanto como
/// AuthenticationStateProvider como IAuthStateNotifier para que la capa
/// Application pueda pedir un refresh tras login/logout sin depender de
/// tipos de ASP.NET Core Components.
///
/// Depende directamente de IUsuarioRepository (no de IAuthService) a
/// proposito: AuthService depende de IAuthStateNotifier, y depender de
/// IAuthService aqui crearia un ciclo en el contenedor de DI.
/// </summary>
public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAuthStateNotifier
{
    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());

    private readonly IUsuarioRepository _usuarioRepository;

    public CustomAuthenticationStateProvider(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var usuario = await _usuarioRepository.GetSesionActivaAsync();
        if (usuario is null)
        {
            return new AuthenticationState(Anonymous);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.AsCodigoUsuario),
            new Claim(ClaimTypes.Name, usuario.AsCodigoUsuario),
            new Claim("vendedor", usuario.Vendedor),
            new Claim("nombre", usuario.Nombre)
        };

        var identity = new ClaimsIdentity(claims, authenticationType: "SistemaILP");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
