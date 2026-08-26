namespace SistemaILP.Ruteo.Application.Configuration;

/// <summary>
/// Información institucional estática mostrada en "Acerca de" (Login y
/// Configuración) - igual que Variant/Version, se centraliza aquí en
/// vez de escribirse a mano en cada pantalla que la muestra.
/// </summary>
public class AppInfoConfiguration
{
    public required string Developer { get; init; }

    public required string SupportEmail { get; init; }

    public required string SupportPhone { get; init; }
}
