namespace SistemaILP.Ruteo.Application.Configuration;

/// <summary>
/// Configuracion central de la aplicacion. Se construye UNA sola vez
/// en el proyecto Maui (el unico lugar que conoce, en tiempo de
/// compilacion, la variante y el entorno activos) y se registra como
/// singleton en el contenedor de DI. Ningun componente Razor ni
/// servicio de negocio debe determinar la variante, la Base URL o la
/// base de datos por su cuenta: todos consumen esta clase.
/// </summary>
public class AppConfiguration
{
    public required AppVariant Variant { get; init; }

    /// <summary>Version visible al usuario, p. ej. "1.0.0". Proviene de ApplicationDisplayVersion del csproj.</summary>
    public required string DisplayVersion { get; init; }

    /// <summary>Numero de build interno, p. ej. "1". Proviene de ApplicationVersion del csproj.</summary>
    public required string BuildNumber { get; init; }

    public required WebServiceConfiguration WebService { get; init; }

    public required DatabaseConfiguration Database { get; init; }

    public string VariantDisplayName => Variant switch
    {
        AppVariant.Preventa => "Preventa",
        AppVariant.Autoventa => "Autoventa",
        AppVariant.Despachos => "Despachos",
        _ => Variant.ToString()
    };
}
