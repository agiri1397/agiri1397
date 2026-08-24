namespace SistemaILP.Ruteo.Application.DTOs;

/// <summary>Credenciales capturadas en el formulario de login.</summary>
public class LoginCredentialsDto
{
    public string Usuario { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Sesion activa expuesta a la UI (proyeccion de la fila de la tabla
/// "usuario" que representa al vendedor actualmente logueado).
/// </summary>
public class SesionUsuarioDto
{
    public string AsCodigoUsuario { get; set; } = string.Empty;

    public string Vendedor { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;
}
