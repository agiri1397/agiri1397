namespace SistemaILP.Ruteo.Domain.Entities;

/// <summary>
/// Corresponde exactamente a la tabla "usuario" de la app Android
/// existente (ver DataBaseContract.UsuarioEntry / DataBaseHelper,
/// version de esquema 26 - la tabla nunca fue alterada desde su
/// creacion). Representa al vendedor con sesion activa en el
/// dispositivo: se crea/actualiza al hacer login y se elimina al
/// hacer logout.
/// </summary>
public class Usuario
{
    public int Id { get; set; }

    /// <summary>Columna "asCodigoUsuario". Usuario de login, UNIQUE.</summary>
    public string AsCodigoUsuario { get; set; } = string.Empty;

    /// <summary>Columna "vendedor". Codigo de vendedor, UNIQUE.</summary>
    public string Vendedor { get; set; } = string.Empty;

    /// <summary>Columna "nombre". Nombre del vendedor.</summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Columna "passW". Se guarda tal como la envia el Web Service /
    /// captura el usuario (texto plano), replicando el comportamiento
    /// real de la app Android existente.
    /// </summary>
    public string PassW { get; set; } = string.Empty;

    /// <summary>Columna "sesionActiva" (0/1).</summary>
    public bool SesionActiva { get; set; }

    /// <summary>Columna "ultimaSincronizacion" (epoch, por ahora sin uso funcional en esta etapa).</summary>
    public long UltimaSincronizacion { get; set; }
}
