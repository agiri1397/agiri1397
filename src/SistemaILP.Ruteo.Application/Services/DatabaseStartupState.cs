namespace SistemaILP.Ruteo.Application.Services;

/// <summary>
/// Resultado de inicializar el esquema de la base de datos, calculado
/// UNA vez de forma bloqueante en MauiProgram, antes de que se
/// construya/renderice ningun componente Razor. Esto es necesario
/// porque CascadingAuthenticationState consulta la tabla "usuario"
/// (via CustomAuthenticationStateProvider) apenas arranca el arbol de
/// componentes - si el esquema todavia no existe en ese momento, la
/// consulta falla con "no such table". Splash.razor lee este estado en
/// vez de volver a inicializar por su cuenta, y solo reintenta cuando
/// el usuario lo solicita explicitamente.
/// </summary>
public class DatabaseStartupState
{
    public bool Succeeded { get; set; }
}
