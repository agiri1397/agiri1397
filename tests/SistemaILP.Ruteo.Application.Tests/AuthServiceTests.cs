using SistemaILP.Ruteo.Application.Common;
using SistemaILP.Ruteo.Application.DTOs;
using SistemaILP.Ruteo.Application.Interfaces;
using SistemaILP.Ruteo.Application.Services;
using SistemaILP.Ruteo.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace SistemaILP.Ruteo.Application.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task LoginAsync_Successful_CreatesActiveSession()
    {
        var repo = new InMemoryUsuarioRepository();
        var wsClient = new FakeLoginWebServiceClient
        {
            Response = new WsLoginResultDto
            {
                Resultado = 1,
                Configuracion = new ConfiguracionDto { CodigoVendedor = "V001", NombreVendedor = "Alice" }
            }
        };
        var sut = new AuthService(wsClient, repo, new NoOpAuthStateNotifier(), NullLogger<AuthService>.Instance);

        var result = await sut.LoginAsync(new LoginCredentialsDto { Usuario = "alice", Password = "secret" });

        Assert.True(result.Succeeded);
        Assert.Equal("Alice", result.Data!.Nombre);

        var sesion = await repo.GetSesionActivaAsync();
        Assert.NotNull(sesion);
        Assert.Equal("alice", sesion!.AsCodigoUsuario);
        Assert.True(sesion.SesionActiva);
    }

    [Fact]
    public async Task LoginAsync_ServerRejects_ReturnsFailureAndNoSession()
    {
        var repo = new InMemoryUsuarioRepository();
        var wsClient = new FakeLoginWebServiceClient
        {
            Response = new WsLoginResultDto { Resultado = 0, Mensaje = "Usuario o contraseña incorrectos" }
        };
        var sut = new AuthService(wsClient, repo, new NoOpAuthStateNotifier(), NullLogger<AuthService>.Instance);

        var result = await sut.LoginAsync(new LoginCredentialsDto { Usuario = "bob", Password = "wrong" });

        Assert.False(result.Succeeded);
        Assert.Equal("Usuario o contraseña incorrectos", result.Error);
        Assert.Null(await repo.GetSesionActivaAsync());
    }

    [Fact]
    public async Task LoginAsync_ExistingUser_UpdatesDataButKeepsStoredPassword()
    {
        // Replica el comportamiento real de Android (LoginService.mangeSuccessfullLogin):
        // un login exitoso de un usuario ya existente NO sobreescribe su contraseña.
        var repo = new InMemoryUsuarioRepository();
        await repo.UpsertAsync(new Usuario
        {
            AsCodigoUsuario = "carol",
            PassW = "original-password",
            Vendedor = "OLD",
            Nombre = "Nombre Viejo",
            SesionActiva = false
        });

        var wsClient = new FakeLoginWebServiceClient
        {
            Response = new WsLoginResultDto
            {
                Resultado = 1,
                Configuracion = new ConfiguracionDto { CodigoVendedor = "V999", NombreVendedor = "Carol Actualizada" }
            }
        };
        var sut = new AuthService(wsClient, repo, new NoOpAuthStateNotifier(), NullLogger<AuthService>.Instance);

        var result = await sut.LoginAsync(new LoginCredentialsDto { Usuario = "carol", Password = "lo-que-tipeo" });

        Assert.True(result.Succeeded);

        var sesion = await repo.GetByCodigoUsuarioAsync("carol");
        Assert.NotNull(sesion);
        Assert.Equal("original-password", sesion!.PassW);
        Assert.Equal("V999", sesion.Vendedor);
        Assert.Equal("Carol Actualizada", sesion.Nombre);
        Assert.True(sesion.SesionActiva);
    }

    [Fact]
    public async Task LogoutAsync_DeletesActiveSessionRow()
    {
        var repo = new InMemoryUsuarioRepository();
        await repo.UpsertAsync(new Usuario
        {
            AsCodigoUsuario = "dave",
            PassW = "x",
            Vendedor = "V1",
            Nombre = "Dave",
            SesionActiva = true
        });

        var sut = new AuthService(new FakeLoginWebServiceClient(), repo, new NoOpAuthStateNotifier(), NullLogger<AuthService>.Instance);

        var result = await sut.LogoutAsync();

        Assert.True(result.Succeeded);
        Assert.Null(await repo.GetSesionActivaAsync());
    }

    private class FakeLoginWebServiceClient : ILoginWebServiceClient
    {
        public WsLoginResultDto? Response { get; set; }

        public Task<Result<WsLoginResultDto>> LoginAsync(WsLoginRequestDto request, CancellationToken cancellationToken = default) =>
            Task.FromResult(Response is null
                ? Result<WsLoginResultDto>.Failure("No configurado")
                : Result<WsLoginResultDto>.Success(Response));
    }

    private class InMemoryUsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _usuarios = new();
        private int _nextId = 1;

        public Task<Usuario?> GetByCodigoUsuarioAsync(string asCodigoUsuario) =>
            Task.FromResult(_usuarios.FirstOrDefault(u => u.AsCodigoUsuario == asCodigoUsuario));

        public Task<Usuario?> GetSesionActivaAsync() =>
            Task.FromResult(_usuarios.FirstOrDefault(u => u.SesionActiva));

        public Task UpsertAsync(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                usuario.Id = _nextId++;
                _usuarios.Add(usuario);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(string asCodigoUsuario)
        {
            _usuarios.RemoveAll(u => u.AsCodigoUsuario == asCodigoUsuario);
            return Task.CompletedTask;
        }
    }

    private class NoOpAuthStateNotifier : IAuthStateNotifier
    {
        public void NotifyAuthenticationStateChanged()
        {
        }
    }
}
