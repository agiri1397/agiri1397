namespace SistemaILP.Ruteo.Application.Configuration;

/// <summary>
/// Configuracion de la base de datos local. Preventa/Autoventa/Despachos
/// comparten la misma base de datos por ahora; PreventaNicaragua (etapa
/// futura) podra usar una distinta sin cambiar esta clase, solo el
/// nombre/ruta con el que se construye.
/// </summary>
public class DatabaseConfiguration
{
    /// <summary>Nombre del archivo SQLite (equivalente a "aurora.db" en Android).</summary>
    public required string DatabaseName { get; init; }

    /// <summary>Ruta absoluta completa, resuelta por el proyecto Maui segun el directorio de datos de la app.</summary>
    public required string FullPath { get; init; }
}
